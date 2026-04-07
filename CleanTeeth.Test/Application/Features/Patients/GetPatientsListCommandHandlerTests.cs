using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Features.Patients.Queries.GetPatientsList;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.ValueObjects;
using NSubstitute;

namespace CleanTeeth.Test.Application.Features.Patients;

[TestClass]
public class GetPatientsListCommandHandlerTests
{
    private IPatientRepository _repository = null!;
    private GetPatientsListQueryHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _repository = Substitute.For<IPatientRepository>();
        _handler = new GetPatientsListQueryHandler(_repository);
    }

    [TestMethod]
    public async Task Handle_ValidQuery_ReturnsPatientsPaginated()
    {
        var page = 1;
        var pageSize = 2;

        var patient1 = new Patient("Patient 1", new Email("patient1@example.com"));
        var patient2 = new Patient("Patient 2", new Email("patient2@example.com"));

        IEnumerable<Patient> patients = new List<Patient> { patient1, patient2 };

        _repository.GetFiltered(Arg.Any<PatientsFilterDto>()).Returns(Task.FromResult(patients));

        _repository.GetFilteredCount(Arg.Any<PatientsFilterDto>()).Returns(Task.FromResult(10));

        var query = new GetPatientsListQuery { Page = page, PageSize = pageSize };
        var result = await _handler.Handle(query);

        Assert.AreEqual(10, result.TotalCount);
    }

    [TestMethod]
    public async Task Handle_WhenThereAreNoPatients_ReturnsEmptyListAndZero()
    {
        IEnumerable<Patient> patients = new List<Patient>();

        _repository.GetFiltered(Arg.Any<PatientsFilterDto>()).Returns(Task.FromResult(patients));

        _repository.GetFilteredCount(Arg.Any<PatientsFilterDto>()).Returns(Task.FromResult(0));
        
        var query = new GetPatientsListQuery { Page = 1, PageSize = 5 };
        
        var result = await _handler.Handle(query);
        
        Assert.AreEqual(0, result.TotalCount);
    }
}