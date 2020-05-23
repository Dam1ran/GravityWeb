using Domain.Entities;
using GravityDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface ICoachService
    {
        Task<IList<CoachDTO>> GetCoachesAsync();
        Task<ApplicationUser> AddPersonalClientAsync(string coachEmail, string clientEmail);
        Task<IList<ClientDTO>> GetPersonalClientsAsync(string coachEmail);
        Task RemoveAllPersonalClientsFromCoachAsync(string coachEmail);
        Task<AppUserCoach> RemovePersonalClientFromCoachAsync(string coachEmail, string clientEmail);
    }
}
