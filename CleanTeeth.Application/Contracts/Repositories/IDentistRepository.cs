using CleanTeeth.Application.Features.Dentists.Queries.GetDentistsList;
using CleanTeeth.Domain.Entites;

namespace CleanTeeth.Application.Contracts.Repositories;

public interface IDentistRepository : IRepository<Dentist>
{
    Task<IEnumerable<Dentist>> GetFiltered(DentistsFilterDto filter);
}