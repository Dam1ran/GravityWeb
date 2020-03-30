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
        Task<ApplicationUser> RemovePersonalClientsFromCoach(string coachEmail);
        Task<ApplicationUser> RemovePersonalClientsFromCoach(string coachEmail, string clientEmail);
    }
}
