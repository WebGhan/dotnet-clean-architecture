using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Application.Features.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand>
{
    private readonly IPatientRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePatientCommandHandler(IPatientRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdatePatientCommand request)
    {
        var patient = await _repository.GetById(request.Id);

        if (patient is null)
        {
            throw new NotFoundException();
        }

        patient.UpdateName(request.Name);
        var email = new Email(request.Email);
        patient.UpdateEmail(email);

        try
        {
            await _repository.Update(patient);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }
}