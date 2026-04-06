using CleanTeeth.Domain.Common;
using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.Entities;

public class DentalOffice: Auditable
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public DentalOffice(string name)
    {
        EnforceNameBusinessRules(name);
        
        Id = Guid.NewGuid();
        Name = name;
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
            throw new BusinessRuleException($"The {nameof(name)} is required！");
        }
    }
}