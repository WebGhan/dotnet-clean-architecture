using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.Dentists.Queries.GetDentistsList;

public class DentistsFilterDto : PagedFilterDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
}