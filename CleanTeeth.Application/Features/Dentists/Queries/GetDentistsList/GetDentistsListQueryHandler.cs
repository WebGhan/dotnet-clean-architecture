using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.Dentists.Queries.GetDentistsList;

public class GetDentistsListQueryHandler: IRequestHandler<GetDentistsListQuery, PagedResult<DentistsListDto>>
{
    private readonly IDentistRepository _repository;

    public GetDentistsListQueryHandler(IDentistRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<DentistsListDto>> Handle(GetDentistsListQuery request)
    {
        var dentists = await _repository.GetFiltered(request);
        var totalCount = await _repository.GetFilteredCount(request);
        var dentistsDto = dentists.Select(dentist => dentist.ToDto()).ToList();

        var pagedResult = new PagedResult<DentistsListDto>
        {
            Items = dentistsDto,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };

        return pagedResult;
    }
}