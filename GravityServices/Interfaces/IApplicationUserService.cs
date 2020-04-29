using GravityDAL.DTO;
using GravityDAL.PageModels;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IApplicationUserService
    {
        Task<PaginatedResult<ApplicationUserDTO>> GetUsers(PaginatedRequest paginatedRequest);

    }
}
