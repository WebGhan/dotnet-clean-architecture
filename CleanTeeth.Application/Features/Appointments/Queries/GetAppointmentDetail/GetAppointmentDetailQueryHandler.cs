using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentDetail;

public class GetAppointmentDetailQueryHandler: IRequestHandler<GetAppointmentDetailQuery, AppointmentDetailDto>
{
    private readonly IAppointmentRepository _repository;

    public GetAppointmentDetailQueryHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<AppointmentDetailDto> Handle(GetAppointmentDetailQuery request)
    {
        var appointment = await _repository.GetById(request.Id);

        if (appointment is null)
        {
            throw new NotFoundException();
        }

        return appointment.ToDto();
    }
}