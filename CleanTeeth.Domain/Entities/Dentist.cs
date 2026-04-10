using CleanTeeth.Domain.Common;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Entities;

public class Dentist: Auditable
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    private readonly List<DentistAssignment> _assignments = new();
    public IReadOnlyCollection<DentistAssignment> Assignments => _assignments.AsReadOnly();

    public Dentist()
    {
    }

    public Dentist(string name, Email email)
    {
        EnforceNameBusinessRules(name);
        EnforceEmailBusinessRules(email);

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
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
            throw new BusinessRuleException($"The {nameof(name)} is required.");
        }
    }

    public void UpdateEmail(Email email)
    {
        EnforceEmailBusinessRules(email);
        Email = email;
    }

    private void EnforceEmailBusinessRules(Email email)
    {
        if (email is null)
        {
            throw new BusinessRuleException($"The {nameof(email)} is required.");
        }
    }
}