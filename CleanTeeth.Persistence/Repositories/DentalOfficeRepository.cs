using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficesList;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Persistence.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Persistence.Repositories;

public class DentalOfficeRepository: Repository<DentalOffice>, IDentalOfficeRepository
{
    private readonly CleanTeethDbContext _dbContext;

    public DentalOfficeRepository(CleanTeethDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DentalOffice>> GetFiltered(DentalOfficesFilterDto filter)
    {
        var queryable = BuildFilteredQuery(filter);
        return await queryable.OrderBy(x => x.Name)
            .Paginate(filter.Page, filter.PageSize)
            .ToListAsync();
    }

    public async Task<int> GetFilteredCount(DentalOfficesFilterDto filter)
    {
        var queryable = BuildFilteredQuery(filter);
        return await queryable.CountAsync();
    }

    private IQueryable<DentalOffice> BuildFilteredQuery(DentalOfficesFilterDto filter)
    {
        var queryable = _dbContext.DentalOffices.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            queryable = queryable.Where(x => x.Name.Contains(filter.Name));
        }

        return queryable;
    }
}