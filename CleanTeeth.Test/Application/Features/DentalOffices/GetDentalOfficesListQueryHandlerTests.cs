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

        _repository.GetFiltered(Arg.Any<DentalOfficesFilterDto>()).Returns(dentalOffices);
        _repository.GetFilteredCount(Arg.Any<DentalOfficesFilterDto>()).Returns(Task.FromResult(dentalOffices.Count));

        var expected = dentalOffices.Select(d => d.ToDto()).ToList();

        var result = await _handler.Handle(new GetDentalOfficesListQuery());

        Assert.AreEqual(expected.Count, result.Items.Count);
        Assert.AreEqual(dentalOffices.Count, result.TotalCount);
        Assert.AreEqual(1, result.Page);
        Assert.AreEqual(10, result.PageSize);

        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i].Id, result.Items[i].Id);
            Assert.AreEqual(expected[i].Name, result.Items[i].Name);
        }
    }

    [TestMethod]
    public async Task Handle_WhenThereAreNoDentalOffices_ReturnsListOfNothing()
    {
        var emptyList = new List<DentalOffice>();

        _repository.GetFiltered(Arg.Any<DentalOfficesFilterDto>()).Returns(emptyList);
        _repository.GetFilteredCount(Arg.Any<DentalOfficesFilterDto>()).Returns(Task.FromResult(0));

        var result = await _handler.Handle(new GetDentalOfficesListQuery());

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Items.Count);
        Assert.AreEqual(0, result.TotalCount);
        Assert.AreEqual(1, result.Page);
        Assert.AreEqual(10, result.PageSize);
    }
}