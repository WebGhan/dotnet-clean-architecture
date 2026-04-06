using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentsList;

public class GetAppointmentsListQueryHandler : IRequestHandler<GetAppointmentsListQuery, List<AppointmentsListDto>>
{
    private readonly IAppointmentRepository _repository;

    public GetAppointmentsListQueryHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<AppointmentsListDto>> Handle(GetAppointmentsListQuery request)
    {
        var appointments = await _repository.GetFiltered(request);
        var appointmentsDto = appointments.Select(appointment => appointment.ToDto()).ToList();
        return appointmentsDto;
    }
}