using CleanTeeth.Application.Contracts.Persistence;

namespace CleanTeeth.Persistence.UnitsOfWork;

public class UnitOfWorkEFCore : IUnitOfWork
{
    private readonly CleanTeethDbContext _dbContext;

    public UnitOfWorkEFCore(CleanTeethDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }

    public Task Rollback()
    {
        return Task.CompletedTask;
    }
}