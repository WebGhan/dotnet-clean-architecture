using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Domain.Entites;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Application.Features.Patients.Commands.CreatePatient;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Guid>
{
    private readonly IPatientRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePatientCommandHandler(IPatientRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreatePatientCommand request)
    {
        var email = new Email(request.Email);
        var patient = new Patient(request.Name, email);

        try
        {
            var result = await _repository.Add(patient);
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