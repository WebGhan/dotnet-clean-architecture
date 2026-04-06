using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Domain.Entites;

namespace CleanTeeth.Persistence.Repositories;

public class DentalOfficeRepository: Repository<DentalOffice>, IDentalOfficeRepository
{
    public DentalOfficeRepository(CleanTeethDbContext dbContext) : base(dbContext)
    {
    }
}