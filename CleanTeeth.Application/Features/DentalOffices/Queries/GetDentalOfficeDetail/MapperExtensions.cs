using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;

public static class MapperExtensions
{
    public static DentalOfficeDetailDto ToDto(this DentalOffice dentalOffice)
    {
        var dto = new DentalOfficeDetailDto
        {
            Id = dentalOffice.Id,
            Name = dentalOffice.Name
        };
        
        return dto;
    }
}