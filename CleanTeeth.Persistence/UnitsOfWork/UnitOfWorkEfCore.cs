using CleanTeeth.Application.Contracts.Persistence;

namespace CleanTeeth.Persistence.UnitsOfWork;

public class UnitOfWorkEfCore : IUnitOfWork
{
    private readonly CleanTeethDbContext _dbContext;

    public UnitOfWorkEfCore(CleanTeethDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }

    public Task Rollback()
    {
        // _dbContext.ChangeTracker.Clear();
        return Task.CompletedTask;
    }
}