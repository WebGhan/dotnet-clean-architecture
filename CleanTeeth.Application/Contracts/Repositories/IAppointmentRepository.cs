using CleanTeeth.Application.Contracts.Repositories.Models;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Contracts.Repositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<bool> OverlapExists(Guid dentistId, DateTime start, DateTime end);

    new Task<Appointment?> GetById(Guid id);

    Task<IEnumerable<Appointment>> GetFiltered(AppointmentsFilterDto appointmentsFilterDto);
    Task<int> GetFilteredCount(AppointmentsFilterDto appointmentsFilterDto);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId);
    Task<Dictionary<Guid, List<Appointment>>> GetByPatientIdsAsync(IEnumerable<Guid> patientIds);
}