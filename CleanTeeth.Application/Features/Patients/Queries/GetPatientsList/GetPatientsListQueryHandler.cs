using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.Patients.Queries.GetPatientsList;

public class GetPatientsListQueryHandler: IRequestHandler<GetPatientsListQuery, PagedResult<PatientListDto>>
{
    private readonly IPatientRepository _repository;

    public GetPatientsListQueryHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<PatientListDto>> Handle(GetPatientsListQuery request)
    {
        var patients = await _repository.GetFiltered(request);
        var totalCount = await _repository.GetFilteredCount(request);
        var patientsDto = patients.Select(patient => patient.ToDto()).ToList();

        var pagedResult = new PagedResult<PatientListDto>
        {
            Items = patientsDto,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };

        return pagedResult;
    }
}