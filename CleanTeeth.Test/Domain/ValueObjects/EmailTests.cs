using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Test.Domain.ValueObjects;

[TestClass]
public class EmailTests
{
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_ThrowsException_WhenEmailIsNull()
    {
        new Email(null!);
    }
    
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_ThrowsException_WhenEmailWithoutAt()
    {
        new Email("han.com");
    }
    
    [TestMethod]
    public void Constructor_ValidEmail_NoException()
    {
        new Email("han@qq.com");
    }
}