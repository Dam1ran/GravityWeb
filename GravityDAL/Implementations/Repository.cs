using AutoMapper;
using Domain.Entities;
using GravityDAL.Helpers;
using GravityDAL.Interfaces;
using GravityDAL.PageModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GravityDAL.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly GravityGymDbContext _gravityGymDbContext;
        private readonly IMapper _mapper;
        protected readonly DbSet<T> _dbSet;
        public Repository(GravityGymDbContext gravityGymDbContext, IMapper mapper)
        {
            _gravityGymDbContext = gravityGymDbContext;
            _mapper = mapper;
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
            return await _gravityGymDbContext.Set<T>().AsNoTracking().ToListAsync();
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
        public async Task<PaginatedResult<TDto>> GetPagedData<TDto>(PaginatedRequest  paginatedRequest) where TDto : class
        {
            return await _gravityGymDbContext.Set<T>().CreatePaginatedResultAsync<T, TDto>(paginatedRequest, _mapper);
        }
        public async Task<T> GetByIdWithInclude(long id, params Expression<Func<T, object>> [] includeProperties)
        {
            var query = IncludeProperties(includeProperties);
            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }
        public IQueryable<T> GetAllWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return IncludeProperties(includeProperties);
        }
        private IQueryable<T> IncludeProperties(params Expression<Func<T, object>> [] includeProperties)
        {
            IQueryable<T> entities = _gravityGymDbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }
    }
}
