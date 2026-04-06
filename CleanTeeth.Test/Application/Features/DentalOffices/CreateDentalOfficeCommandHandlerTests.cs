using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Domain.Entities;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace CleanTeeth.Test.Application.Features.DentalOffices;

[TestClass]
public class CreateDentalOfficeCommandHandlerTests
{
    private IDentalOfficeRepository _repository = null!;
    private IUnitOfWork _unitOfWork = null!;
    private CreateDentalOfficeCommandHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _repository = Substitute.For<IDentalOfficeRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new CreateDentalOfficeCommandHandler(_repository, _unitOfWork);
    }

    [TestMethod]
    public async Task Handle_ValidCommand_ReturnsDentalOfficeId()
    {
        var command = new CreateDentalOfficeCommand { Name = "Example A" };
        
        var dentalOffice = new DentalOffice("Example A");
        
        _repository.Add(Arg.Any<DentalOffice>()).Returns(dentalOffice);

        var result = await _handler.Handle(command);
        
        await _repository.Received(1).Add(Arg.Any<DentalOffice>());
        await _unitOfWork.Received(1).Commit();
        Assert.AreEqual(dentalOffice.Id, result);
    }
    
    [TestMethod]
    public async Task Handle_WhenTheresAnError_Rollback()
    {
        var command = new CreateDentalOfficeCommand { Name = "Example B" };
        
        _repository.Add(Arg.Any<DentalOffice>()).Throws<Exception>();
        
        await Assert.ThrowsExceptionAsync<Exception>(async () =>
        {
            await _handler.Handle(command);
        });
        
        await _unitOfWork.Received(1).Rollback();
    }
}