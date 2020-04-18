using Domain.Entities.WorkoutEntities;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravityDAL.Implementations
{
    public class WoRoutineRepository : Repository<WoRoutine>, IWoRoutineRepository
    {
        public WoRoutineRepository(GravityGymDbContext gravityGymDbContext) : base(gravityGymDbContext)
        {
                        
        }

        public async Task<WoRoutine> GetByIdWithInclude(long id)
        {
            return await _dbSet.Where(x => x.Id == id)
                .Include(x => x.Workouts)
                .FirstOrDefaultAsync();

        }
    }
}
