using CleanTeeth.Domain.Entites;

namespace CleanTeeth.Application.Features.Dentists.Queries.GetDentistsDetail;

internal static class MapperExtensions
{
    internal static DentistDetailDto toDto(this Dentist dentist)
    {
        return new DentistDetailDto
        {
            Id = dentist.Id,
            Name = dentist.Name,
            Email = dentist.Email.Value
        };
    }
}