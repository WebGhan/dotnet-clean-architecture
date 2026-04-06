using CleanTeeth.Domain.Common;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Entites;

public class Patient: Auditable
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;

    public Patient()
    {
    }

    public Patient(string name, Email email)
    {
        EnforceNameBusinessRules(name);
        EnforceEmailBusinessRules(email);
        
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }
    
    public void UpdateName(string name)
    {
        EnforceNameBusinessRules(name);
        Name = name;
    }

    private void EnforceNameBusinessRules(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessRuleException($"The {nameof(name)} is required.");
        }
    }

    public void UpdateEmail(Email email)
    {
        EnforceEmailBusinessRules(email);
        Email = email;
    }

    private void EnforceEmailBusinessRules(Email email)
    {
        if (email is null)
        {
            throw new BusinessRuleException($"The {nameof(email)} is required.");
        }
    }
}