namespace CleanTeeth.Application.Utilities.Common;

public abstract class PagedFilterDto
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}