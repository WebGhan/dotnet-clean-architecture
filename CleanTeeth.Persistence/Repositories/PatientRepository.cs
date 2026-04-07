using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Features.Patients.Queries.GetPatientsList;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Persistence.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Persistence.Repositories;

public class PatientRepository : Repository<Patient>, IPatientRepository
{
    private readonly CleanTeethDbContext _dbContext;

    public PatientRepository(CleanTeethDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Patient>> GetFiltered(PatientsFilterDto filter)
    {
        var queryable = BuildFilteredQuery(filter);
        return await queryable.OrderBy(p => p.Name)
            .Paginate(filter.Page, filter.PageSize)
            .ToListAsync();
    }

    public async Task<int> GetFilteredCount(PatientsFilterDto filter)
    {
        var queryable = BuildFilteredQuery(filter);
        return await queryable.CountAsync();
    }

    private IQueryable<Patient> BuildFilteredQuery(PatientsFilterDto filter)
    {
        var queryable = _dbContext.Patients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            queryable = queryable.Where(p => p.Name.Contains(filter.Name));
        }

        if (!string.IsNullOrWhiteSpace(filter.Email))
        {
            queryable = queryable.Where(p => p.Email.Value.Contains(filter.Email));
        }

        return queryable;
    }
}