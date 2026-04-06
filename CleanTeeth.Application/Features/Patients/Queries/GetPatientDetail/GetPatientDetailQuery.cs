using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Patients.Queries.GetPatientDetail;

public class GetPatientDetailQuery: IRequest<PatientDetailDto>
{
   public required Guid Id { get; set; }
}