using Family.Domain.Entities.Base;

namespace Family.Domain.Repositories.Abstractions;

public interface IRepository<TEntity> where TEntity : Entity 
{
    public Task<TEntity?> GetByIdAsync(long id);
    public Task<TEntity> CreateAsync(string name);
    public Task<TEntity> UpdateAsync(long id, TEntity entity);
    public Task<TEntity> DeleteAsync(long id);
}