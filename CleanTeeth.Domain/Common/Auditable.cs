namespace CleanTeeth.Domain.Common;

public abstract class Auditable
{
    public string? CreatedBy { get; set; }
    // 这里使用 CreatedAt 更规范
    public DateTime? CreatedOn { get; set; }
    // 这里使用 UpdatedBy 更规范
    public string? LastModifiedBy { get; set; }
    // 这里使用 UpdatedAt 更规范
    public DateTime? LastModifiedOn { get; set; }
}