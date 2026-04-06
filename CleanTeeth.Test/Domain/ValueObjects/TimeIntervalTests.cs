using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Test.Domain.ValueObjects;

[TestClass]
public class TimeIntervalTests
{
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_StartIsAfterEnd_ThrowsException()
    {
        new TimeInterval(DateTime.UtcNow, DateTime.UtcNow.AddDays(-1));
    }

    [TestMethod]
    public void Constructor_ValidTimeInterval_NoException()
    {
        new TimeInterval(DateTime.UtcNow, DateTime.UtcNow.AddHours(1));
    }
    
}