using Domain.Entities;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GravityDAL.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly GravityGymDbContext _gravityGymDbContext;
        protected readonly DbSet<T> _dbSet;
        public Repository(GravityGymDbContext gravityGymDbContext)
        {
            _gravityGymDbContext = gravityGymDbContext;
            _dbSet = _gravityGymDbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            _gravityGymDbContext.Set<T>().Add(entity);
            await _gravityGymDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _gravityGymDbContext.Set<T>().Remove(entity);

            await _gravityGymDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> DeleteAsync(long id)
        {
            var entity = await _gravityGymDbContext.Set<T>().FindAsync(id);

            if (entity == null)
            {
                throw new Exception($"Object of type {typeof(T)} with id { id } not found");
            }

            return await DeleteAsync(entity);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _gravityGymDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _gravityGymDbContext.FindAsync<T>(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _gravityGymDbContext.SaveChangesAsync() >= 0;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _gravityGymDbContext.Entry(entity).State = EntityState.Modified;
            await _gravityGymDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
