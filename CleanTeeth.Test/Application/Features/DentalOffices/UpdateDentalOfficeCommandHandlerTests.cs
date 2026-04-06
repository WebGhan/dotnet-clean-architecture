using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Features.DentalOffices.Commands.UpdateDentalOffice;
using CleanTeeth.Domain.Entities;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace CleanTeeth.Test.Application.Features.DentalOffices;

[TestClass]
public class UpdateDentalOfficeCommandHandlerTests
{
    private IDentalOfficeRepository _repository = null!;
    private IUnitOfWork _unitOfWork = null!;
    private UpdateDentalOfficeCommandHandler _handler = null!;

    [TestInitialize]
    public void Initialize()
    {
        _repository = Substitute.For<IDentalOfficeRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new UpdateDentalOfficeCommandHandler(_repository, _unitOfWork);
    }

    [TestMethod]
    public async Task Handle_WhenDentalOfficeExists_ShouldUpdateDentalOffice()
    {
        var dentalOffice = new DentalOffice("Dental Office A");
        var id = dentalOffice.Id;
        var command = new UpdateDentalOfficeCommand { Id = id, Name = "New name" };

        _repository.GetById(id).Returns(dentalOffice);

        await _handler.Handle(command);

        await _repository.Received(1).Update(dentalOffice);
        await _unitOfWork.Received(1).Commit();
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task Handle_WhenDentalOfficeDoesNotExist_Throws()
    {
        var command = new UpdateDentalOfficeCommand { Id = Guid.NewGuid(), Name = "New name" };
        _repository.GetById(command.Id).ReturnsNull();

        await _handler.Handle(command);
    }

    [TestMethod]
    public async Task Handle_WhenThereIsAnException_RollbackIsCalled()
    {
        var dentalOffice = new DentalOffice("Dental Office A");
        var id = dentalOffice.Id;
        var command = new UpdateDentalOfficeCommand { Id = id, Name = "New name" };
        
        _repository.GetById(id).Returns(dentalOffice);
        _repository.Update(dentalOffice).Throws(new InvalidOperationException("Exception"));
        
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _handler.Handle(command));
        
        await _unitOfWork.Received(1).Rollback();
    }
}