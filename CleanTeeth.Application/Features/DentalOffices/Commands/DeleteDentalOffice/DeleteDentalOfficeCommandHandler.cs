using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.DeleteDentalOffice;

public class DeleteDentalOfficeCommandHandler : IRequestHandler<DeleteDentalOfficeCommand>
{
    private readonly IDentalOfficeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDentalOfficeCommandHandler(IDentalOfficeRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteDentalOfficeCommand request)
    {
        var dentalOffice = await _repository.GetById(request.Id);

        if (dentalOffice is null)
        {
            throw new NotFoundException();
        }

        try
        {
            await _repository.Delete(dentalOffice);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }
}