using CleanTeeth.Domain.Entites;
using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Test.Domain.Entities;

[TestClass]
public class DentalOfficeTests
{
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_NullName_Exception()
    {
        new DentalOffice(null!);
    }
}