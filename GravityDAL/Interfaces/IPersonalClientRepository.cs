using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IPersonalClientRepository : IRepository<PersonalClient>
    {
        IQueryable<PersonalClient> GetPersonalClients();
        IQueryable<PersonalClient> GetPersonalClients(long coachEmail);
        Task<bool> RemovePersonalClients(IList<PersonalClient> personalClientList);
        Task<bool> RemovePersonalClient(PersonalClient personalClient);
    }
}
