using Domain.Entities;
using GravityDAL.Interfaces;

namespace GravityDAL.Implementations
{
    public class MuscleRepository : Repository<Muscle>, IMuscleRepository
    {
        public MuscleRepository(GravityGymDbContext gravityGymDbContext) : base(gravityGymDbContext)
        {

        }
    }
}
