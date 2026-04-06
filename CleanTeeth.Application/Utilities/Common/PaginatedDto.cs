namespace CleanTeeth.Application.Utilities.Common;

public class PaginatedDto<T>
{
    public List<T> Elements { get; set; } = [];
    public int TotalAmountOfRecords { get; set; }
}