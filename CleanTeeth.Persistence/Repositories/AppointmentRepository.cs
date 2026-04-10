using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Contracts.Repositories.Models;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Enums;
using CleanTeeth.Persistence.Utilities;
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

    public async Task<IEnumerable<Appointment>> GetFiltered(AppointmentsFilterDto filter)
    {
        var queryable = BuildFilteredQuery(filter);
        return await queryable.OrderBy(x => x.TimeInterval.Start)
            .Paginate(filter.Page, filter.PageSize)
            .ToListAsync();
    }

    public async Task<int> GetFilteredCount(AppointmentsFilterDto filter)
    {
        var queryable = BuildFilteredQuery(filter);
        return await queryable.CountAsync();
    }

    private IQueryable<Appointment> BuildFilteredQuery(AppointmentsFilterDto filter)
    {
        var queryable = _dbContext.Appointments
            .Include(x => x.DentalOffice)
            .Include(x => x.Dentist)
            .Include(x => x.Patient)
            .AsQueryable();

        if (filter.DentalOfficeId is not null)
        {
            queryable = queryable.Where(x => x.DentalOfficeId == filter.DentalOfficeId);
        }

        if (filter.DentistId is not null)
        {
            queryable = queryable.Where(x => x.DentistId == filter.DentistId);
        }

        if (filter.PatientId is not null)
        {
            queryable = queryable.Where(x => x.PatientId == filter.PatientId);
        }

        if (filter.AppointmentStatus is not null)
        {
            queryable = queryable.Where(x => x.Status == filter.AppointmentStatus);
        }

        queryable = queryable.Where(x =>
            x.TimeInterval.Start >= filter.StartDate.UtcDateTime &&
            x.TimeInterval.End <= filter.EndDate.UtcDateTime);

        return queryable;
    }

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId)
    {
        return await _dbContext.Appointments
            .Where(x => x.PatientId == patientId)
            .Include(x => x.Patient)
            .Include(x => x.Dentist)
            .Include(x => x.DentalOffice)
            .OrderBy(x => x.TimeInterval.Start)
            .ToListAsync();
    }

    public async Task<Dictionary<Guid, List<Appointment>>> GetByPatientIdsAsync(IEnumerable<Guid> patientIds)
    {
        var patientIdList = patientIds.ToList();
        if (!patientIdList.Any())
        {
            return new Dictionary<Guid, List<Appointment>>();
        }

        var appointments = await _dbContext.Appointments
            .Where(x => patientIdList.Contains(x.PatientId))
            .Include(x => x.Patient)
            .Include(x => x.Dentist)
            .Include(x => x.DentalOffice)
            .OrderBy(x => x.TimeInterval.Start)
            .ToListAsync();

        return appointments
            .GroupBy(x => x.PatientId)
            .ToDictionary(g => g.Key, g => g.ToList());
    }
}