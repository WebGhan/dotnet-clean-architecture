using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficesList;

public class GetDentalOfficesListQuery : DentalOfficesFilterDto, IRequest<PagedResult<DentalOfficesListDto>>
{
}