using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Test.Domain.Entities;

[TestClass]
public class PatientTests
{
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_NullName_Exception()
    {
        var email = new Email("han@qq.com");
        new Patient(null!, email);
    }
    
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_NullEmail_Exception()
    {
        new Patient("Han",null!);
    }
    
    [TestMethod]
    public void Constructor_ValidPatient_NoException()
    {
        var email = new Email("han@qq.com");
        new Patient("Han",email);
    }
}