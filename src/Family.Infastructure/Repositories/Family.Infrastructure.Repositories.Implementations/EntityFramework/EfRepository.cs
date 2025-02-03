using Family.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Family.Infrastructure.Repositories.Implementations.EntityFramework;

public class EfRepository<TEntity, TId>(FamilyDbContext context) : IRepository<TEntity, TId> where TEntity : Entity<TId> where TId : struct
{
    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public IQueryable<TEntity> GetAll()
    {
        return context.Set<TEntity>().AsQueryable();
    }
    
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }
    
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
    
    public async Task DeleteAsync(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TId id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
            await DeleteAsync(entity);
    }
}