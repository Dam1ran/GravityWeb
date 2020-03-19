using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteAsync(long id);
        Task<bool> SaveChangesAsync();

    }
}
