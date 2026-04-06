using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Features.Patients.Queries.GetPatientDetail;

internal static class MapperExtensions
{
    internal static PatientDetailDto ToDto(this Patient patient)
    {
        return new PatientDetailDto()
        {
            Id = patient.Id,
            Name = patient.Name,
            Email = patient.Email.Value
        };
    }
}