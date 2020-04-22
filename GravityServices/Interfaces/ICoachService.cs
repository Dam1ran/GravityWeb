using Domain.Entities;
using GravityDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface ICoachService
    {
        Task<IList<CoachDTO>> GetCoachesAsync();
        Task<ApplicationUser> AddPersonalClient(string coachEmail, string clientEmail);
        Task<IList<ClientDTO>> GetPersonalClients(string coachEmail);
        Task<bool> RemoveAllPersonalClientsFromCoach(string coachEmail);
        Task<bool> RemovePersonalClientFromCoach(string coachEmail, string clientEmail);
    }
}
