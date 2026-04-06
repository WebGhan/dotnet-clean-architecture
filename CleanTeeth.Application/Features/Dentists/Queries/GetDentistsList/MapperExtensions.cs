using CleanTeeth.Domain.Entites;

namespace CleanTeeth.Application.Features.Dentists.Queries.GetDentistsList;

internal static class MapperExtensions
{
    internal static DentistsListDto ToDto(this Dentist dentist)
    {
        return new DentistsListDto
        {
            Id = dentist.Id,
            Name = dentist.Name,
            Email = dentist.Email.Value
        };
    }
}