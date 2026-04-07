using CleanTeeth.API.Dtos.Patients;
using CleanTeeth.Application.Features.Patients.Commands.CreatePatient;
using CleanTeeth.Application.Features.Patients.Commands.DeletePatient;
using CleanTeeth.Application.Features.Patients.Commands.UpdatePatient;
using CleanTeeth.Application.Features.Patients.Queries.GetPatientDetail;
using CleanTeeth.Application.Features.Patients.Queries.GetPatientsList;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;
using Microsoft.AspNetCore.Mvc;

namespace CleanTeeth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientDetailDto>> GetPatientDetail(Guid id)
    {
        var query = new GetPatientDetailQuery { Id = id };
        return await _mediator.Send(query);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<PatientListDto>>> Get([FromQuery] GetPatientsListQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDto createPatientDto)
    {
        var command = new CreatePatientCommand
        {
            Name = createPatientDto.Name,
            Email = createPatientDto.Email,
        };

        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] UpdatePatientDto updatePatientDto)
    {
        var command = new UpdatePatientCommand
        {
            Id = id,
            Name = updatePatientDto.Name,
            Email = updatePatientDto.Email,
        };

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(Guid id)
    {
        var command = new DeletePatientCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}