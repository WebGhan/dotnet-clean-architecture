using CleanTeeth.Application.Features.Dentists.Queries.GetDentistsDetail;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;

public static class MapperExtensions
{
    public static DentalOfficeDetailDto ToDto(this DentalOffice dentalOffice)
    {
        var dto = new DentalOfficeDetailDto
        {
            Id = dentalOffice.Id,
            Name = dentalOffice.Name,
            Dentists = dentalOffice.Assignments
                .Select(a => a.Dentist)
                .Where(dentist => dentist != null)
                .Select(dentist => new DentistDetailDto
                {
                    Id = dentist.Id,
                    Name = dentist.Name,
                    Email = dentist.Email.Value
                })
                .ToList()
        };

        return dto;
    }
}