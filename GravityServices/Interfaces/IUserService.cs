using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IUserService
    {        
        Task<bool> UpdateRoleAsync(string userEmail, long roleId);
    }
}
