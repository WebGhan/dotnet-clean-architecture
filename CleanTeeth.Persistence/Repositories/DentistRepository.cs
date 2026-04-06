using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Features.Dentists.Queries.GetDentistsList;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Persistence.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Persistence.Repositories;

public class DentistRepository : Repository<Dentist>, IDentistRepository
{
    private readonly CleanTeethDbContext _dbContext;

    public DentistRepository(CleanTeethDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Dentist>> GetFiltered(DentistsFilterDto filter)
    {
        var queryable = _dbContext.Dentists.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            queryable = queryable.Where(p => p.Name.Contains(filter.Name));
        }

        if (!string.IsNullOrWhiteSpace(filter.Email))
        {
            queryable = queryable.Where(p => p.Email.Value.Contains(filter.Email));
        }

        return await queryable.OrderBy(p => p.Name)
            .Paginate(filter.Page, filter.RecordsPerPage)
            .ToListAsync();
    }
}