using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Domain.Entities;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CleanTeeth.Test.Application.Features.DentalOffices;

[TestClass]
public class GetDentalOfficeDetailQueryHandlerTests
{
    private IDentalOfficeRepository _repository = null!;
    private GetDentalOfficeDetailQueryHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _repository = Substitute.For<IDentalOfficeRepository>();
        _handler = new GetDentalOfficeDetailQueryHandler(_repository);
    }

    [TestMethod]
    public async Task Handle_DentalOfficeExists_ReturnsIt()
    {
        var dentalOffice = new DentalOffice("Dental Office A");
        var id = dentalOffice.Id;
        var query = new GetDentalOfficeDetailQuery { Id = id };

        _repository.GetById(id).Returns(dentalOffice);

        var result = await _handler.Handle(query);

        Assert.IsNotNull(result);
        Assert.AreEqual(id, result.Id);
        Assert.AreEqual("Dental Office A", result.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task Handle_DentalOfficeDoesNotExist_Throws()
    {
        var id = Guid.NewGuid();
        var query = new GetDentalOfficeDetailQuery { Id = id };

        _repository.GetById(id).ReturnsNull();
        
        await _handler.Handle(query);
    }
}