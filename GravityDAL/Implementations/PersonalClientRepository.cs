using Domain.Entities;
using GravityDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravityDAL.Implementations
{
    public class PersonalClientRepository : Repository<PersonalClient>, IPersonalClientRepository
    {
        public PersonalClientRepository(GravityGymDbContext gravityGymDbContext) : base(gravityGymDbContext)
        {

        }

        public IQueryable<PersonalClient> GetPersonalClients()
        {
            return _dbSet;
        }

        public IQueryable<PersonalClient> GetPersonalClients(long coachId)
        {
            return _dbSet.Where(pc=>pc.ApplicationUserId==coachId);
        }

        public async Task<bool> RemovePersonalClient(PersonalClient personalClient)
        {
            _dbSet.Remove(personalClient);

            return await SaveChangesAsync();
        }

        public async Task<bool> RemovePersonalClients(IList<PersonalClient> personalClientList)
        {
            _dbSet.RemoveRange(personalClientList);

            return await SaveChangesAsync();
        }

    }
}
