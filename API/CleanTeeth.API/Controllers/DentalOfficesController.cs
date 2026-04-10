using CleanTeeth.API.Dtos.DentalOffices;
using CleanTeeth.Application.Features.DentalOffices.Commands.AssignDentist;
using CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Application.Features.DentalOffices.Commands.DeleteDentalOffice;
using CleanTeeth.Application.Features.DentalOffices.Commands.UpdateDentalOffice;
using CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficesList;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;
using Microsoft.AspNetCore.Mvc;

namespace CleanTeeth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class DentalOfficesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DentalOfficesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<DentalOfficesListDto>>> GetListAsync([FromQuery] GetDentalOfficesListQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DentalOfficeDetailDto>> GetByIdAsync(Guid id)
    {
        var query = new GetDentalOfficeDetailQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateDentalOfficeDto createDentalOfficeDto)
    {
        var command = new CreateDentalOfficeCommand { Name = createDentalOfficeDto.Name };
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateDentalOfficeDto updateDentalOfficeDto)
    {
        var command = new UpdateDentalOfficeCommand
        {
            Id = id,
            Name = updateDentalOfficeDto.Name
        };

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var command = new DeleteDentalOfficeCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id}/assign-dentist")]
    public async Task<IActionResult> AssignDentistAsync(Guid id, [FromBody] AssignDentistDto assignDentistDto)
    {
        var command = new AssignDentistToDentalOfficeCommand
        {
            DentalOfficeId = id,
            DentistId = assignDentistDto.DentistId
        };
        await _mediator.Send(command);
        return Ok();
    }
}