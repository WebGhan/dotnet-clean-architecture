using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficesList;
using CleanTeeth.Domain.Entities;
using NSubstitute;

namespace CleanTeeth.Test.Application.Features.DentalOffices;

[TestClass]
public class GetDentalOfficesListQueryHandlerTests
{
    private IDentalOfficeRepository _repository = null!;
    private GetDentalOfficesListQueryHandler _handler = null!;

    [TestInitialize]
    public void Initialize()
    {
        _repository = Substitute.For<IDentalOfficeRepository>();
        _handler = new GetDentalOfficesListQueryHandler(_repository);
    }

    [TestMethod]
    public async Task Handle_WhenThereAreDentalOffices_ReturnsListOfThem()
    {
        var dentalOffices = new List<DentalOffice>
        {
            new DentalOffice("Dental Office A"),
            new DentalOffice("Dental Office B"),
        };

        _repository.GetAll().Returns(dentalOffices);

        var expected = dentalOffices.Select(d => d.ToDto()).ToList();

        var result = await _handler.Handle(new GetDentalOfficesListQuery());

        Assert.AreEqual(expected.Count, result.Count);

        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i].Id, result[i].Id);
            Assert.AreEqual(expected[i].Name, result[i].Name);
        }
    }

    [TestMethod]
    public async Task Handle_WhenThereAreNoDentalOffices_ReturnsListOfNothing()
    {
        _repository.GetAll().Returns(new List<DentalOffice>());
        
        var result = await _handler.Handle(new GetDentalOfficesListQuery());
        
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }
}