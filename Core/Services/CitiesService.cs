using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Data;
using Project.Entities;
using Project.Repositories;

namespace Project.Services
{
    public class CitiesService : GenericRepository<City>, ICitiesService
    {
        private readonly ApplicationDbContext _dbContext;
        public CitiesService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<City> GetWithProvince(int id)
        {
            return await _dbContext.Cities.Include(i => i.Province).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IReadOnlyList<City>> GetAllWithProvince()
        {
            return await _dbContext.Cities.Include(i => i.Province).ToListAsync();
        }
    }
}
