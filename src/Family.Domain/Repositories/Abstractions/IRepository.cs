using System.Linq;
using System.Threading.Tasks;
using Family.Domain.Entities.Base;

namespace Family.Domain.Repositories.Abstractions;

public interface IRepository<TEntity> where TEntity : Entity 
{
    public Task<TEntity?> GetByIdAsync(long id);
    public IQueryable<TEntity> GetAll();
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task DeleteAsync(TEntity entity);
    public Task DeleteAsync(long id); 
}