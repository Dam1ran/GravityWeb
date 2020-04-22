using AutoMapper;
using Domain.Entities;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GravityDAL.Implementations
{
    public class AppUserCoachRepository : Repository<AppUserCoach>, IAppUserCoachRepository
    {
        private readonly ILogger<AppUserCoachRepository> _logger;

        public AppUserCoachRepository(GravityGymDbContext gravityGymDbContext,
            ILogger<AppUserCoachRepository> logger,
            IMapper mapper) : base(gravityGymDbContext,mapper)
        {
            _logger = logger;
        }
        
        public async Task<bool> RemoveAllPersonalClientsFromCoach(long coachId)
        {
            var clientsToRemove = await _dbSet.Where(c => c.CoachId == coachId).ToListAsync();
            try
            {
                _dbSet.RemoveRange(clientsToRemove);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> RemovePersonalClientFromCoach(long coachId, long clientId)
        {
            var userCoach = await _dbSet.Where(x=>x.CoachId==coachId && x.ApplicationUserId==clientId).FirstOrDefaultAsync();
            try
            {
                await DeleteAsync(userCoach.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
