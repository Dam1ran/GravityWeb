using Domain.Entities;
using GravityDAL.PageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<PaginatedResult<TDto>> GetPagedDataAsync<TDto>(PaginatedRequest  paginatedRequest) where TDto : class;
        Task<T> GetByIdAsync(long id);
        Task<T> GetByIdWithIncludeAsync(long id, params Expression<Func<T, object>> [] includeProperties);
        IQueryable<T> GetAllWithIncludeAsync(params Expression<Func<T, object>> [] includeProperties);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteAsync(long id);
        Task<bool> SaveChangesAsync();

    }
}
