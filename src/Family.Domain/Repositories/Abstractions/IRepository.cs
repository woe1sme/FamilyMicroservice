using System.Linq;
using System.Threading.Tasks;
using Family.Domain.Entities.Base;

namespace Family.Domain.Repositories.Abstractions;

public interface IRepository<TEntity, in TId> where TEntity : Entity<TId> where TId : struct
{
    public Task<TEntity?> GetByIdAsync(TId id);
    public IQueryable<TEntity> GetAll();
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task DeleteAsync(TEntity entity);
    public Task DeleteAsync(TId id); 
}