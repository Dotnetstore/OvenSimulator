using Dotnetstore.OvenSimulator.SharedKernel.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.OvenSimulator.SharedKernel.Repositories;

public sealed class GenericRepository<T>(DbContext context) : IGenericRepository<T>
    where T : BaseAuditableEntity
{
    public IQueryable<T> Entities => context.Set<T>();
    
    async Task<T?> IGenericRepository<T>.GetByIdAsync(Guid id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    async Task<List<T>> IGenericRepository<T>.GetAllAsync()
    {
        return await context
            .Set<T>()
            .ToListAsync();
    }

    void IGenericRepository<T>.Create(T entity)
    {
        context.Add(entity);
    }

    void IGenericRepository<T>.Update(T entity)
    {
        context.Update(entity);
    }

    void IGenericRepository<T>.Delete(T entity)
    {
        context.Remove(entity);
    }
}