using CleanTeeth.API.Dtos.Dentists;
using CleanTeeth.API.Utilities;
using CleanTeeth.Application.Features.Dentists.Commands.CreateDentist;
using CleanTeeth.Application.Features.Dentists.Commands.DeleteDentist;
using CleanTeeth.Application.Features.Dentists.Commands.UpdateDentist;
using CleanTeeth.Application.Features.Dentists.Queries.GetDentistsDetail;
using CleanTeeth.Application.Features.Dentists.Queries.GetDentistsList;
using CleanTeeth.Application.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CleanTeeth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DentistsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DentistsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DentistDetailDto>> Get(Guid id)
    {
        var query = new GetDentistDetailQuery { Id = id };
        return await _mediator.Send(query);
    }

    [HttpGet]
    public async Task<ActionResult<List<DentistsListDto>>> Get([FromQuery] GetDentistsListQuery query)
    {
        var result = await _mediator.Send(query);
        HttpContext.InsertPaginationInformationInHeader(result.TotalAmountOfRecords);
        return result.Elements;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDentistDto createDentistDto)
    {
        var command = new CreateDentistCommand
        {
            Name = createDentistDto.Name,
            Email = createDentistDto.Email,
        };

        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateDentistDto updateDentistDto)
    {
        var command = new UpdateDentistCommand
        {
            Id = id,
            Name = updateDentistDto.Name,
            Email = updateDentistDto.Email,
        };

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteDentistCommand
        {
            Id = id
        };
        await _mediator.Send(command);
        return NoContent();
    }
}