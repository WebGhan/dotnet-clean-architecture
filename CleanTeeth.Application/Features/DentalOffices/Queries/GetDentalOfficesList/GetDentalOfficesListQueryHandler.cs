using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficesList;

public class GetDentalOfficesListQueryHandler: IRequestHandler<GetDentalOfficesListQuery, PagedResult<DentalOfficesListDto>>
{
    private readonly IDentalOfficeRepository _repository;

    public GetDentalOfficesListQueryHandler(IDentalOfficeRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<DentalOfficesListDto>> Handle(GetDentalOfficesListQuery request)
    {
        var dentalOffices = await _repository.GetFiltered(request);
        var totalCount = await _repository.GetFilteredCount(request);
        var dentalOfficesDto = dentalOffices.Select(dentalOffice => dentalOffice.ToDto()).ToList();

        var pagedResult = new PagedResult<DentalOfficesListDto>
        {
            Items = dentalOfficesDto,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };

        return pagedResult;
    }
}