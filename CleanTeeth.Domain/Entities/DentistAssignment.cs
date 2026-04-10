using CleanTeeth.Domain.Common;
using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.Entities;

public class DentistAssignment : Auditable
{
    public Guid Id { get; private set; }
    public Guid DentistId { get; private set; }
    public Guid DentalOfficeId { get; private set; }

    // Navigation properties
    public Dentist? Dentist { get; private set; }
    public DentalOffice? DentalOffice { get; private set; }

    private DentistAssignment()
    {
    }

    public DentistAssignment(Guid dentistId, Guid dentalOfficeId)
    {
        if (dentistId == Guid.Empty)
        {
            throw new BusinessRuleException($"{nameof(dentistId)} is required.");
        }

        if (dentalOfficeId == Guid.Empty)
        {
            throw new BusinessRuleException($"{nameof(dentalOfficeId)} is required.");
        }

        Id = Guid.NewGuid();
        DentistId = dentistId;
        DentalOfficeId = dentalOfficeId;
    }

    // Optionally, add methods to update if needed
}