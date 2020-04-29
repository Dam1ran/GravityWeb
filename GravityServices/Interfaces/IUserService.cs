using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IUserService
    {        
        Task<bool> UpdateRole(string userEmail, long roleId);

    }
}
