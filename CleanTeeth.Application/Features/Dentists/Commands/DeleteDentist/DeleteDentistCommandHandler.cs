using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Dentists.Commands.DeleteDentist;

public class DeleteDentistCommandHandler : IRequestHandler<DeleteDentistCommand>
{
    private readonly IDentistRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDentistCommandHandler(IDentistRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteDentistCommand request)
    {
        var dentist = await _repository.GetById(request.Id);

        if (dentist is null)
        {
            throw new NotFoundException();
        }

        try
        {
            await _repository.Delete(dentist);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }
}