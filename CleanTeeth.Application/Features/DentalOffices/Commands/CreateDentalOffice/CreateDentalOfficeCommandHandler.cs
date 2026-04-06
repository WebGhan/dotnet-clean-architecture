using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice;

public class CreateDentalOfficeCommandHandler: IRequestHandler<CreateDentalOfficeCommand, Guid>
{
    private readonly IDentalOfficeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    // private readonly IValidator<CreateDentalOfficeCommand> _validator;

    public CreateDentalOfficeCommandHandler(IDentalOfficeRepository repository, IUnitOfWork unitOfWork)
        // IValidator<CreateDentalOfficeCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        // _validator = validator;
    }

    public async Task<Guid> Handle(CreateDentalOfficeCommand command)
    {
        // var validationResult = await _validator.ValidateAsync(command);
        //
        // if (!validationResult.IsValid)
        // {
        //     throw new CustomValidationException(validationResult);
        // }
        
        var dentalOffice = new DentalOffice(command.Name);

        try
        {
            var result = await _repository.Add(dentalOffice);
            await _unitOfWork.Commit();
            return result.Id;
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }
}