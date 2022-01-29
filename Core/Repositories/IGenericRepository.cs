using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Models;

namespace Project.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task Recover(T entity);
    }
}
