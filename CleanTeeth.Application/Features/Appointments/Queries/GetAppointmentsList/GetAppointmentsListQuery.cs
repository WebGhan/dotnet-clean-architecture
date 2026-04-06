using CleanTeeth.Application.Contracts.Repositories.Models;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentsList;

public class GetAppointmentsListQuery: AppointmentsFilterDto, IRequest<List<AppointmentsListDto>>
{
}