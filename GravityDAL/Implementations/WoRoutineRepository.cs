using AutoMapper;
using Domain.Entities.WorkoutEntities;
using GravityDAL.Interfaces;

namespace GravityDAL.Implementations
{
    public class WoRoutineRepository : Repository<WoRoutine>, IWoRoutineRepository
    {
        public WoRoutineRepository(GravityGymDbContext gravityGymDbContext, IMapper mapper) : base(gravityGymDbContext, mapper)
        {
                        
        }

    }
}

