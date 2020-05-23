using AutoMapper;
using Domain.Entities.WorkoutEntities;
using GravityDAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GravityDAL.Repositories
{
    public class RoutineRepository : Repository<Routine>, IRoutineRepository
    {
        public RoutineRepository(GravityGymDbContext gravityGymDbContext, IMapper mapper) : base(gravityGymDbContext, mapper)
        {                        
        }

        public async Task<Workout> GetLastWorkoutFromRoutineAsync(long routineId)
        {
            var routine = await GetByIdWithIncludeAsync(routineId, x => x.Workouts);

            if (routine == null)
            {
                throw new Exception($"Object of type {typeof(Workout).ToString().Split('.').Last()} with id { routineId } not found");
            }

            return routine.Workouts.OrderBy(x => x.Order).LastOrDefault();
        }
    }
}

