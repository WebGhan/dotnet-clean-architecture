using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Features.DentalOffices.Commands.DeleteDentalOffice;
using CleanTeeth.Domain.Entites;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace CleanTeeth.Test.Application.Features.DentalOffices;

[TestClass]
public class DeleteDentalOfficeCommandHandlerTests
{
    private IDentalOfficeRepository _repository = null!;
    private IUnitOfWork _unitOfWork = null!;
    private DeleteDentalOfficeCommandHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _repository = Substitute.For<IDentalOfficeRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new DeleteDentalOfficeCommandHandler(_repository, _unitOfWork);
    }

    [TestMethod]
    public async Task Handle_WhenDentalOfficeExists_ShouldDeleteDentalOffice()
    {
        var dentalOffice = new DentalOffice("Dental Office A");
        var command = new DeleteDentalOfficeCommand { Id = dentalOffice.Id };

        _repository.GetById(command.Id).Returns(dentalOffice);

        await _handler.Handle(command);

        await _repository.Received(1).Delete(dentalOffice);
        await _unitOfWork.Received(1).Commit();
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task Handle_WhenDentalOfficeDoNotExists_Throws()
    {
        var command = new DeleteDentalOfficeCommand { Id = Guid.NewGuid() };
        _repository.GetById(command.Id).ReturnsNull();
        await _handler.Handle(command);
    }

    [TestMethod]
    public async Task Handle_WhenAnExceptionOccursWhileUpdating_RollbackIsCalled()
    {
        var dentalOffice = new DentalOffice("Dental Office A");
        var command = new DeleteDentalOfficeCommand { Id = dentalOffice.Id };

        _repository.GetById(command.Id).Returns(dentalOffice);
        _repository.Delete(dentalOffice).Throws(new InvalidOperationException("Exception"));

        await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _handler.Handle(command));
        await _unitOfWork.Received(1).Rollback();
    }
}