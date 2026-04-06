using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Dentists.Commands.UpdateDentist;

public class UpdateDentistCommandHandler : IRequestHandler<UpdateDentistCommand>
{
    private readonly IDentistRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDentistCommandHandler(IDentistRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateDentistCommand request)
    {
        var dentist = await _repository.GetById(request.Id);

        if (dentist is null)
        {
            throw new NotFoundException();
        }

        dentist.UpdateName(request.Name);
        var email = dentist.Email;
        dentist.UpdateEmail(email);

        try
        {
            await _repository.Update(dentist);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }
}