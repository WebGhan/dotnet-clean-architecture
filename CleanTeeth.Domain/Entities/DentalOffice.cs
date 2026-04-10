using CleanTeeth.Domain.Common;
using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.Entities;

public class DentalOffice: Auditable
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    private readonly List<DentistAssignment> _assignments = new();
    public IReadOnlyCollection<DentistAssignment> Assignments => _assignments.AsReadOnly();

    public DentalOffice(string name)
    {
        EnforceNameBusinessRules(name);
        
        Id = Guid.NewGuid();
        Name = name;
    }

    public void UpdateName(string name)
    {
        EnforceNameBusinessRules(name);
        Name = name;
    }

    private void EnforceNameBusinessRules(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessRuleException($"The {nameof(name)} is required！");
        }
    }

    public void AssignDentist(Guid dentistId)
    {
        // Check if already assigned
        if (_assignments.Any(a => a.DentistId == dentistId))
        {
            throw new BusinessRuleException($"Dentist {dentistId} is already assigned to this dental office.");
        }

        var assignment = new DentistAssignment(dentistId, Id);
        _assignments.Add(assignment);
    }

    public void RemoveDentistAssignment(Guid dentistId)
    {
        var assignment = _assignments.FirstOrDefault(a => a.DentistId == dentistId);
        if (assignment != null)
        {
            _assignments.Remove(assignment);
        }
    }

    public IEnumerable<Guid> GetAssignedDentistIds() => _assignments.Select(a => a.DentistId);
}