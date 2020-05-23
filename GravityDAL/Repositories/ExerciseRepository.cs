using AutoMapper;
using Domain.Entities.WorkoutEntities;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravityDAL.Repositories
{
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(GravityGymDbContext gravityGymDbContext, IMapper mapper) : base(gravityGymDbContext, mapper)
        {
        }

        public async Task<IList<Exercise>> GetByWorkoutIdAsync(long Id)
        {
            var exercises = await _dbSet
                .Include(x => x.ExerciseTemplate) 
                .Include(x => x.Sets)
                .Where(x => x.WorkoutId == Id)
                .OrderBy(x => x.Order)
                .ToListAsync();

            foreach (var exercise in exercises)
            {
                exercise.Sets = exercise.Sets.OrderBy(x => x.Order).ToList();
            }

            return exercises;
        }

        public async Task<Exercise> GetFirstWithOrderSuperiorToAsync(long workoutId, int order)
        {
            return await _dbSet
                .Where(x => (x.WorkoutId == workoutId) && (x.Order > order))
                .OrderBy(x => x.Order)
                .FirstOrDefaultAsync();
        }

        public async Task<Exercise> GetLastWithOrderInferiorToAsync(long workoutId, int order)
        {
            return await _dbSet
                .Where(x => (x.WorkoutId == workoutId) && (x.Order < order))
                .OrderBy(x=>x.Order)
                .LastOrDefaultAsync();
        }
    }
}
