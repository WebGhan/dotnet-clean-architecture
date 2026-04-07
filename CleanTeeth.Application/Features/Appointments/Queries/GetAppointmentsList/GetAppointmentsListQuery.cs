using CleanTeeth.Application.Contracts.Repositories.Models;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentsList;

public class GetAppointmentsListQuery : AppointmentsFilterDto, IRequest<PagedResult<AppointmentsListDto>>
{
}