using AutoMapper;
using Domain.Entities.WorkoutEntities;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GravityDAL.Repositories
{
    public class SetRepository : Repository<Set>, ISetRepository
    {
        public SetRepository(GravityGymDbContext gravityGymDbContext, IMapper mapper) : base(gravityGymDbContext, mapper)
        {
            

        }

        public async Task<int> GetNextSetOrderOfExerciseAsync(long exercisesId)
        {
            var set = await _dbSet
                .Where(x => x.ExerciseId == exercisesId)
                .OrderBy(x => x.Order)
                .LastOrDefaultAsync();

            if (set != null)
            {
                return set.Order+1;
            }

            return 0;
        }
    }
}
