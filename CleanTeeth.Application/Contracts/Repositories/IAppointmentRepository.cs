using CleanTeeth.Application.Contracts.Repositories.Models;
using CleanTeeth.Domain.Entites;

namespace CleanTeeth.Application.Contracts.Repositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<bool> OverlapExists(Guid dentistId, DateTime start, DateTime end);

    new Task<Appointment?> GetById(Guid id);

    Task<IEnumerable<Appointment>> GetFiltered(AppointmentsFilterDto appointmentsFilterDto);
}