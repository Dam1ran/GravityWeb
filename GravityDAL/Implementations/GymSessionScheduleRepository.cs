using Domain.Entities;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravityDAL.Implementations
{
    public class GymSessionScheduleRepository : Repository<GymSessionSchedule>, IGymSessionScheduleRepository
    {
        //private readonly IGymSessionScheduleRepository _gymSessionScheduleRepository;

        public GymSessionScheduleRepository(
            GravityGymDbContext gravityGymDbContext
           /*,IGymSessionScheduleRepository gymSessionScheduleRepository*/) : base(gravityGymDbContext)
        {
            //_gymSessionScheduleRepository = gymSessionScheduleRepository;
        }

        public async Task<IList<GymSessionSchedule>> GetByDayOfWeek(DayOfWeek dayOfWeek)
        {
            var listOfSessionsPerDayOfWeek = await _dbSet.Where(session => session.DayOfWeek == dayOfWeek).ToListAsync();
            
            return listOfSessionsPerDayOfWeek;
        }
    }
}
