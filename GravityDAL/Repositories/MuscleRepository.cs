using AutoMapper;
using Domain.Entities;
using GravityDAL.Interfaces;

namespace GravityDAL.Repositories
{
    public class MuscleRepository : Repository<Muscle>, IMuscleRepository
    {
        public MuscleRepository(GravityGymDbContext gravityGymDbContext, IMapper mapper) : base(gravityGymDbContext, mapper)
        {

        }
    }
}
