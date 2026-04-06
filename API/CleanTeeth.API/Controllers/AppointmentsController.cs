using CleanTeeth.API.Dtos.Appointments;
using CleanTeeth.Application.Features.Appointments.Commands.CancelAppointment;
using CleanTeeth.Application.Features.Appointments.Commands.CompleteAppointment;
using CleanTeeth.Application.Features.Appointments.Commands.CreateAppointment;
using CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentDetail;
using CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentsList;
using CleanTeeth.Application.Utilities;
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
    public async Task<ActionResult<AppointmentDetailDto>> Get(Guid id)
    {
        var query = new GetAppointmentDetailQuery
        {
            Id = id
        };

        return await _mediator.Send(query);
    }

    [HttpGet]
    public async Task<ActionResult<List<AppointmentsListDto>>> Get([FromQuery] GetAppointmentsListQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto createAppointmentDto)
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
    public async Task<IActionResult> CompleteAppointment(Guid id)
    {
        var command = new CompleteAppointmentCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelAppointment(Guid id)
    {
        var command = new CancelAppointmentCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}