using CleanTeeth.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly CleanTeethDbContext _dbContext;

    public Repository(CleanTeethDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public Task<T> Add(T entity)
    {
        _dbContext.Add(entity);
        return Task.FromResult(entity);
    }

    public Task Update(T entity)
    {
        _dbContext.Update(entity);
        return Task.CompletedTask;
    }

    public Task Delete(T entity)
    {
        _dbContext.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<int> GetTotalAmountOfRecords()
    {
        return await _dbContext.Set<T>().CountAsync();
    }
}