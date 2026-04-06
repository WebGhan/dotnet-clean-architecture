using CleanTeeth.Domain.Entites;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Test.Domain.Entities;

[TestClass]
public class DentistTests
{
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_NullName_Exception()
    {
        var email = new Email("han@qq.com");
        new Dentist(null!, email);
    }
    
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_NullEmail_Exception()
    {
        new Dentist("Han",null!);
    }
    
    [TestMethod]
    public void Constructor_ValidDentist_NoException()
    {
        var email = new Email("han@qq.com");
        new Dentist("Han",email);
    }
}