using CleanTeeth.Domain.Common;
using CleanTeeth.Domain.Enums;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Entites;

public class Appointment: Auditable
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public Guid DentistId { get; private set; }
    public Guid DentalOfficeId { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public TimeInterval TimeInterval { get; private set; } = null!;
    public Patient? Patient { get; private set; }
    public Dentist? Dentist { get; private set; }
    public DentalOffice? DentalOffice { get; private set; }

    private Appointment()
    {
    }

    public Appointment(Guid patientId, Guid dentistId, Guid dentalOfficeId, TimeInterval timeInterval)
    {
        if (timeInterval.Start < DateTime.UtcNow)
        {
            throw new BusinessRuleException("The start time cannot be in the past.");
        }
        
        Id = Guid.NewGuid();
        Status = AppointmentStatus.Scheduled;
        PatientId = patientId;
        DentistId = dentistId;
        DentalOfficeId = dentalOfficeId;
        TimeInterval = timeInterval;
    }

    public void Cancel()
    {
        if (Status != AppointmentStatus.Scheduled)
        {
            throw new BusinessRuleException("Only scheduled appointments can be cancelled.");
        }
        
        Status = AppointmentStatus.Cancelled;
    }

    public void Complete()
    {
        if (Status != AppointmentStatus.Scheduled)
        {
            throw new BusinessRuleException("Only scheduled appointments can be completed.");
        }
        
        Status = AppointmentStatus.Completed;
    }
}
