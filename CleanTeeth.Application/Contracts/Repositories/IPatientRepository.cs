using CleanTeeth.Application.Features.Patients.Queries.GetPatientsList;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Contracts.Repositories;

public interface IPatientRepository : IRepository<Patient>
{
    Task<IEnumerable<Patient>> GetFiltered(PatientsFilterDto filter);
}