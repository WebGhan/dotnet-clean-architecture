using CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficesList;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Contracts.Repositories;

public interface IDentalOfficeRepository : IRepository<DentalOffice>
{
    Task<IEnumerable<DentalOffice>> GetFiltered(DentalOfficesFilterDto filter);
    Task<int> GetFilteredCount(DentalOfficesFilterDto filter);
}