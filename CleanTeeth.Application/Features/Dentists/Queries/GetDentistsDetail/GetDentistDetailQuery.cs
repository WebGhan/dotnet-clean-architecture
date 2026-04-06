using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Dentists.Queries.GetDentistsDetail;

public class GetDentistDetailQuery : IRequest<DentistDetailDto>
{
    public required Guid Id { get; set; }
}