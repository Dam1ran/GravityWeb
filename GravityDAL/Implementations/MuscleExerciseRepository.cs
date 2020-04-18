using Domain.Entities;
using GravityDAL.Interfaces;

namespace GravityDAL.Implementations
{
    public class MuscleExerciseRepository :Repository<MuscleExercise>, IMuscleExerciseRepository
    {
        public MuscleExerciseRepository(GravityGymDbContext gravityGymDbContext) : base(gravityGymDbContext)
        {

        }
    }
}
