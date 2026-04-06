using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.Dentists.Queries.GetDentistsList;

public class GetDentistsListQuery : DentistsFilterDto, IRequest<PaginatedDto<DentistsListDto>>
{
    
}