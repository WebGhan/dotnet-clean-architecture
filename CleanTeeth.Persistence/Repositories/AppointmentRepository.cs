using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Contracts.Repositories.Models;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Persistence.Repositories;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    private readonly CleanTeethDbContext _dbContext;

    public AppointmentRepository(CleanTeethDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> OverlapExists(Guid dentistId, DateTime start, DateTime end)
    {
        return await _dbContext.Appointments
            .Where(x => x.DentistId == dentistId
                        && x.Status == AppointmentStatus.Scheduled
                        && start < x.TimeInterval.End && end > x.TimeInterval.Start)
            .AnyAsync();
    }

    public new async Task<Appointment?> GetById(Guid id)
    {
        return await _dbContext.Appointments
            .Include(x => x.Patient)
            .Include(x => x.Dentist)
            .Include(x => x.DentalOffice)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Appointment>> GetFiltered(AppointmentsFilterDto appointmentsFilterDto)
    {
        var queryable = _dbContext.Appointments
            .Include(x => x.DentalOffice)
            .Include(x => x.Dentist)
            .Include(x => x.Patient)
            .AsQueryable();

        if (appointmentsFilterDto.DentalOfficeId is not null)
        {
            queryable = queryable.Where(x => x.DentalOfficeId == appointmentsFilterDto.DentalOfficeId);
        }

        if (appointmentsFilterDto.DentistId is not null)
        {
            queryable = queryable.Where(x => x.DentistId == appointmentsFilterDto.DentistId);
        }

        if (appointmentsFilterDto.PatientId is not null)
        {
            queryable = queryable.Where(x => x.PatientId == appointmentsFilterDto.PatientId);
        }

        if (appointmentsFilterDto.AppointmentStatus is not null)
        {
            queryable = queryable.Where(x => x.Status == appointmentsFilterDto.AppointmentStatus);
        }

        return await queryable.Where(x =>
                x.TimeInterval.Start >= appointmentsFilterDto.StartDate.UtcDateTime &&
                x.TimeInterval.End <= appointmentsFilterDto.EndDate.UtcDateTime)
            .OrderBy(x => x.TimeInterval.Start)
            .ToListAsync();
    }
}