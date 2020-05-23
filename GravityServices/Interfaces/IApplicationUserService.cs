using GravityDAL.Models;
using GravityDAL.PageModels;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IApplicationUserService
    {
        Task<PaginatedResult<ApplicationUserModel>> GetUsersAsync(PaginatedRequest paginatedRequest);
    }
}
