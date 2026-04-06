using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficesList;

public static class MapperExtensions
{
    public static DentalOfficesListDto ToDto(this DentalOffice dentalOffice)
    {
        var dto = new DentalOfficesListDto
        {
            Id = dentalOffice.Id,
            Name = dentalOffice.Name,
        };
        
        return dto;
    }
}