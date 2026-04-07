using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficesList;

public class DentalOfficesFilterDto : PagedFilterDto
{
    public string? Name { get; set; }
}