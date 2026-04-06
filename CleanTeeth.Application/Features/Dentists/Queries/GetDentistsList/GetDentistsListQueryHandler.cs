using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.Dentists.Queries.GetDentistsList;

public class GetDentistsListQueryHandler: IRequestHandler<GetDentistsListQuery, PaginatedDto<DentistsListDto>>
{
    private readonly IDentistRepository _repository;

    public GetDentistsListQueryHandler(IDentistRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedDto<DentistsListDto>> Handle(GetDentistsListQuery request)
    {
        var dentists = await _repository.GetFiltered(request);
        var totalAmountOfRecords = await _repository.GetTotalAmountOfRecords();
        var dentistsDto = dentists.Select(dentist => dentist.ToDto()).ToList();

        var paginatedDto = new PaginatedDto<DentistsListDto>
        {
            Elements = dentistsDto,
            TotalAmountOfRecords = totalAmountOfRecords
        };
        
        return paginatedDto;
    }
}