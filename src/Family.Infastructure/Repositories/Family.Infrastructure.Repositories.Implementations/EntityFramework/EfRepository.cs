using Family.Domain.Entities.Base;
using Family.Domain.Repositories.Abstractions;
using Family.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Family.Infrastructure.Repositories.Implementations.EntityFramework;

public class EfRepository<TEntity>(FamilyDbContext context) : IRepository<TEntity> where TEntity : Entity
{
    public async Task<TEntity?> GetByIdAsync(long id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }
    
    public async Task<IQueryable<TEntity>> GetAllAsync()
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
    
    public async Task DeleteAsync(long id)
    {
        var entity = await GetByIdAsync(id);
        if(entity == null)
            return;
        await DeleteAsync(entity);
    }
}