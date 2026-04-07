using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentsList;

public class GetAppointmentsListQueryHandler : IRequestHandler<GetAppointmentsListQuery, PagedResult<AppointmentsListDto>>
{
    private readonly IAppointmentRepository _repository;

    public GetAppointmentsListQueryHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<AppointmentsListDto>> Handle(GetAppointmentsListQuery request)
    {
        var appointments = await _repository.GetFiltered(request);
        var totalCount = await _repository.GetFilteredCount(request);
        var appointmentsDto = appointments.Select(appointment => appointment.ToDto()).ToList();

        var pagedResult = new PagedResult<AppointmentsListDto>
        {
            Items = appointmentsDto,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };

        return pagedResult;
    }
}