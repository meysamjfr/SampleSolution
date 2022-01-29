using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Entities;

namespace Project.Repositories
{
    public interface IProvincesService : IGenericRepository<Province>
    {
        Task<IReadOnlyList<Province>> GetAllWithCities();
        Task<Province> GetWithCities(int id);
    }
}
