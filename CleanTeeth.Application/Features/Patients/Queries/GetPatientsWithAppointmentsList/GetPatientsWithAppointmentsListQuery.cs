using CleanTeeth.Application.Features.Patients.Queries.GetPatientsList;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.Patients.Queries.GetPatientsWithAppointmentsList;

public class GetPatientsWithAppointmentsListQuery : PatientsFilterDto, IRequest<PagedResult<PatientWithAppointmentsListDto>>
{
}