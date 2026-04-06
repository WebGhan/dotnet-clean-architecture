using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentDetail;

public class GetAppointmentDetailQuery: IRequest<AppointmentDetailDto>
{
    public required Guid Id { get; set; }
}