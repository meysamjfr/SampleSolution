using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Entities;

namespace Project.Repositories
{
    public interface ICitiesService : IGenericRepository<City>
    {
        Task<IReadOnlyList<City>> GetAllWithProvince();
        Task<City> GetWithProvince(int id);
    }
}
