using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.UpdateDentalOffice;

public class UpdateDentalOfficeCommandHandler: IRequestHandler<UpdateDentalOfficeCommand>
{
    private readonly IDentalOfficeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDentalOfficeCommandHandler(IDentalOfficeRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateDentalOfficeCommand request)
    {
        var dentalOffice = await _repository.GetById(request.Id);

        if (dentalOffice is null)
        {
            throw new NotFoundException();
        }
        
        dentalOffice.UpdateName(request.Name);

        try
        {
            await _repository.Update(dentalOffice);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }
}