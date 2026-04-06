using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Patients.Queries.GetPatientDetail;

public class GetPatientDetailQueryHandler : IRequestHandler<GetPatientDetailQuery, PatientDetailDto>
{
    private readonly IPatientRepository _repository;

    public GetPatientDetailQueryHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<PatientDetailDto> Handle(GetPatientDetailQuery request)
    {
        var patient = await _repository.GetById(request.Id);

        if (patient is null)
        {
            throw new NotFoundException();
        }

        return patient.ToDto();
    }
}