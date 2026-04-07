using CleanTeeth.API.Dtos.DentalOffices;
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
    public async Task<ActionResult<PagedResult<DentalOfficesListDto>>> Get([FromQuery] GetDentalOfficesListQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DentalOfficeDetailDto>> Get(Guid id)
    {
        var query = new GetDentalOfficeDetailQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDentalOfficeDto createDentalOfficeDto)
    {
        var command = new CreateDentalOfficeCommand { Name = createDentalOfficeDto.Name };
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateDentalOfficeDto updateDentalOfficeDto)
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
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteDentalOfficeCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}