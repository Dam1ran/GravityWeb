using Domain.Entities;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IAppUserCoachRepository : IRepository<AppUserCoach>
    {
        Task<AppUserCoach> RemovePersonalClientFromCoachAsync(long coachId, long clientId);
        Task RemoveAllPersonalClientsFromCoachAsync(long coachId);
    }
}
