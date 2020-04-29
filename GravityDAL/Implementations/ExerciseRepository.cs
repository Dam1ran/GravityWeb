using AutoMapper;
using Domain.Entities.WorkoutEntities;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravityDAL.Implementations
{
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(GravityGymDbContext gravityGymDbContext, IMapper mapper) : base(gravityGymDbContext, mapper)
        {

        }

        public async Task<IList<Exercise>> GetByWorkoutId(long Id)
        {
            return await _dbSet
                .Include(x => x.ExerciseTemplate) 
                .Where(x => x.WorkoutId == Id)
                .OrderBy(x => x.Order)
                .ToListAsync();
        }

        public async Task<Exercise> GetFirstWithOrderSuperiorTo(long workoutId, int order)
        {
            return await _dbSet
                .Where(x => (x.WorkoutId == workoutId) && (x.Order > order))
                .OrderBy(x => x.Order)
                .FirstOrDefaultAsync();
        }

        public async Task<Exercise> GetLastWithOrderInferiorTo(long workoutId, int order)
        {
            return await _dbSet
                .Where(x => (x.WorkoutId == workoutId) && (x.Order < order))
                .OrderBy(x=>x.Order)
                .LastOrDefaultAsync();
        }
    }
}
