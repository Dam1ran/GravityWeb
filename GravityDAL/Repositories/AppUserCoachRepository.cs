using AutoMapper;
using Domain.Entities;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GravityDAL.Repositories
{
    public class AppUserCoachRepository : Repository<AppUserCoach>, IAppUserCoachRepository
    {

        public AppUserCoachRepository(GravityGymDbContext gravityGymDbContext,
            IMapper mapper) : base(gravityGymDbContext,mapper) { }
        
        public async Task RemoveAllPersonalClientsFromCoachAsync(long coachId)
        {
            var clientsToRemove = await _dbSet.Where(c => c.CoachId == coachId).ToListAsync();
            _dbSet.RemoveRange(clientsToRemove);            
        }

        public async Task<AppUserCoach> RemovePersonalClientFromCoachAsync(long coachId, long clientId)
        {
            var userCoach = await _dbSet.Where(x=>x.CoachId==coachId && x.ApplicationUserId==clientId).FirstOrDefaultAsync();
            return await DeleteAsync(userCoach.Id);            
        }
    }
}
