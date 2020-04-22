using AutoMapper;
using Domain.Entities;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravityDAL.Implementations
{
    public class GymSessionScheduleRepository : Repository<GymSessionSchedule>, IGymSessionScheduleRepository
    {
        
        public GymSessionScheduleRepository(GravityGymDbContext gravityGymDbContext, IMapper mapper) : base(gravityGymDbContext, mapper)
        {
            
        }

        public async Task<IList<GymSessionSchedule>> GetByDayOfWeek(string dayOfWeek)
        {
            var listOfSessionsPerDayOfWeek = await _dbSet.Where(session => session.DayOfWeek == dayOfWeek)
                .OrderBy(h=>h.Time)
                .ThenBy(p=>p.Practice)
                .ToListAsync();
            
            return listOfSessionsPerDayOfWeek;
        }
    }
}
