using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficesList;

public class GetDentalOfficesListQueryHandler: IRequestHandler<GetDentalOfficesListQuery, List<DentalOfficesListDto>>
{
    private readonly IDentalOfficeRepository _repository;

    public GetDentalOfficesListQueryHandler(IDentalOfficeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DentalOfficesListDto>> Handle(GetDentalOfficesListQuery request)
    {
        var dentalOffices = await _repository.GetAll();
        var dentalOfficesDto = dentalOffices.Select(dentalOffice => dentalOffice.ToDto()).ToList();
        
        return dentalOfficesDto;
    }
}