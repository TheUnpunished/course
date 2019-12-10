using Authentification.Repositories.Entities;
using System.Threading.Tasks;

namespace Authentification.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(long id);
        Task<T[]> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> EditAsync(T entity);
        Task<T> RemoveAsync(long id);
        Task<T> AddId(T entity);
    }
}
