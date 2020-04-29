using Domain.Entities;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IAppUserCoachRepository : IRepository<AppUserCoach>
    {
        Task<bool> RemovePersonalClientFromCoach(long coachId, long clientId);
        Task<bool> RemoveAllPersonalClientsFromCoach(long coachId);
    }
}
