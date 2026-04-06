using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Dentists.Queries.GetDentistsDetail;

public class GetDentistDetailQueryHandler : IRequestHandler<GetDentistDetailQuery, DentistDetailDto>
{
    private readonly IDentistRepository _repository;

    public GetDentistDetailQueryHandler(IDentistRepository repository)
    {
        _repository = repository;
    }

    public async Task<DentistDetailDto> Handle(GetDentistDetailQuery request)
    {
        var dentist = await _repository.GetById(request.Id);
        
        if (dentist is null)
        {
            throw new NotFoundException();
        }
        
        return dentist.toDto();
    }
}