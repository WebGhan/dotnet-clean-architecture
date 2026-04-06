using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Features.Patients.Queries.GetPatientsList;

internal static class MapperExtensions
{
    internal static PatientListDto ToDto(this Patient patient)
    {
        return new PatientListDto
        {
            Id = patient.Id,
            Name = patient.Name,
            Email = patient.Email.Value
        };
    }
}