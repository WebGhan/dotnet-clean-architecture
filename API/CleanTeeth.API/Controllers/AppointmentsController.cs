using CleanTeeth.API.Dtos.Appointments;
using CleanTeeth.Application.Features.Appointments.Commands.CancelAppointment;
using CleanTeeth.Application.Features.Appointments.Commands.CompleteAppointment;
using CleanTeeth.Application.Features.Appointments.Commands.CreateAppointment;
using CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentDetail;
using CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentsList;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;
using Microsoft.AspNetCore.Mvc;

namespace CleanTeeth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentDetailDto>> GetByIdAsync(Guid id)
    {
        var query = new GetAppointmentDetailQuery
        {
            Id = id
        };

        return await _mediator.Send(query);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<AppointmentsListDto>>> GetListAsync([FromQuery] GetAppointmentsListQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAppointmentDto createAppointmentDto)
    {
        var command = new CreateAppointmentCommand
        {
            PatientId = createAppointmentDto.PatientId,
            DentistId = createAppointmentDto.DentistId,
            DentalOfficeId = createAppointmentDto.DentalOfficeId,
            StartDate = createAppointmentDto.StartDate.UtcDateTime,
            EndDate = createAppointmentDto.EndDate.UtcDateTime,
        };

        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> CompleteAsync(Guid id)
    {
        var command = new CompleteAppointmentCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelAsync(Guid id)
    {
        var command = new CancelAppointmentCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}