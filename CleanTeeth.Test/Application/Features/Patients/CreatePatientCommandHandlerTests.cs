using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Features.Patients.Commands.CreatePatient;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.ValueObjects;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace CleanTeeth.Test.Application.Features.Patients;

[TestClass]
public class CreatePatientCommandHandlerTests
{
    private IPatientRepository _repository = null!;
    private IUnitOfWork _unitOfWork = null!;
    private CreatePatientCommandHandler _handler = null!;

    [TestInitialize]
    public void Initialize()
    {
        _repository = Substitute.For<IPatientRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new CreatePatientCommandHandler(_repository, _unitOfWork);
    }

    [TestMethod]
    public async Task Handle_ValidCommand_ReturnsPatientId()
    {
        var command = new CreatePatientCommand { Name = "test", Email = "test@example.com" };
        var patient = new Patient(command.Name, new Email(command.Email));

        _repository.Add(Arg.Any<Patient>()).Returns(patient);

        var result = await _handler.Handle(command);

        Assert.AreEqual(patient.Id, result);
        await _repository.Received(1).Add(Arg.Any<Patient>());
        await _unitOfWork.Received(1).Commit();
    }

    [TestMethod]
    public async Task Handle_WhenTheresAnError_WeRollBack()
    {
        var command = new CreatePatientCommand { Name = "test", Email = "test@example.com" };

        _repository.Add(Arg.Any<Patient>()).Throws<Exception>();

        await Assert.ThrowsExceptionAsync<Exception>(async () => await _handler.Handle(command));

        await _unitOfWork.Received(1).Rollback();
    }
}