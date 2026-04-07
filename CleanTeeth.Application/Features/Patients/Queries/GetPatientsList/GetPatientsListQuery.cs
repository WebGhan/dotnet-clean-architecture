using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.Patients.Queries.GetPatientsList;

public class GetPatientsListQuery : PatientsFilterDto, IRequest<PagedResult<PatientListDto>>
{
}