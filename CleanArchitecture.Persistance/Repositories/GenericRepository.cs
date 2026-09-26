using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories;

public  class GenericRepository<T> : IGenericRepository<T> where T : Entity
{
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public void Add(T entity) => _dbSet.Add(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Remove(T entity) => _dbSet.Remove(entity);

    public IQueryable<T> GetAll() => _dbSet.AsNoTracking();
}