using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.ValueObjects;

public class TimeInterval
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public TimeInterval(DateTime start, DateTime end)
    {
        if (start > end)
        {
            throw new BusinessRuleException("The start time cannot be after the end time.");
        }
        
        Start = start;
        End = end;
    }
}