using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Data;
using Project.Models;
using Project.Repositories;

namespace Project.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Get(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            var d = entity as Entities.Base.BaseEntity;
            d.IsActive = true;
            _dbContext.Entry(d).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Recover(T entity)
        {
            var d = entity as Entities.Base.BaseEntity;
            d.IsActive = true;
            _dbContext.Entry(d).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
