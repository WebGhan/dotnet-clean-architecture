namespace CleanTeeth.Application.Features.Dentists.Queries.GetDentistsList;

public class DentistsListDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}