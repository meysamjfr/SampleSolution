using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Data;
using Project.Entities;
using Project.Repositories;

namespace Project.Services
{
    public class ProvincesService : GenericRepository<Province>, IProvincesService
    {
        private readonly ApplicationDbContext _dbContext;
        public ProvincesService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Province>> GetAllWithCities()
        {
            return await _dbContext.Provinces.Include(i => i.Cities).ToListAsync();
        }

        public async Task<Province> GetWithCities(int id)
        {
            return await _dbContext.Provinces.Include(i => i.Cities).FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
