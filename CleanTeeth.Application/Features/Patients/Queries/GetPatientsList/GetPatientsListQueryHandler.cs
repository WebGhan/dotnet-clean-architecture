using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.Patients.Queries.GetPatientsList;

public class GetPatientsListQueryHandler: IRequestHandler<GetPatientsListQuery, PaginatedDto<PatientListDto>>
{
    private readonly IPatientRepository _repository;

    public GetPatientsListQueryHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedDto<PatientListDto>> Handle(GetPatientsListQuery request)
    {
        var patients = await _repository.GetFiltered(request);
        var totalAmountOfRecords = await _repository.GetTotalAmountOfRecords();
        var patientsDto = patients.Select(patient => patient.ToDto()).ToList();

        var paginatedDto = new PaginatedDto<PatientListDto>
        {
            Elements = patientsDto,
            TotalAmountOfRecords = totalAmountOfRecords,
        };
        
        return paginatedDto;
    }
}