using HungryMonster.Core.Entities;
using HungryMonster.Core.Interfaces;
using HungryMonster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HungryMonster.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly HungryMonsterDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(HungryMonsterDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    /// <summary>Returns all entities as a read-only list (no change tracking).</summary>
    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _dbSet.AsNoTracking().ToListAsync();

    /// <summary>Returns the entity with the given id, or null if not found.</summary>
    public async Task<T?> GetByIdAsync(int id) =>
        await _dbSet.FindAsync(id);

    /// <summary>Persists a new entity and returns it with the generated Id.</summary>
    public async Task<T> AddAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>Updates an existing tracked entity.</summary>
    public async Task UpdateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>Deletes the entity with the given id. No-op if not found.</summary>
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is not null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
