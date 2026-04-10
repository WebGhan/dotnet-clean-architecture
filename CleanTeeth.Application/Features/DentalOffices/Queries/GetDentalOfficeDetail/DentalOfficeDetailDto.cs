using CleanTeeth.Application.Features.Dentists.Queries.GetDentistsDetail;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;

public class DentalOfficeDetailDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public List<DentistDetailDto> Dentists { get; set; } = new();
}