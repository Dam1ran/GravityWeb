using AutoMapper;
using Domain.Entities.WorkoutEntities;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GravityDAL.Implementations
{
    public class WorkoutRepository : Repository<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(GravityGymDbContext gravityGymDbContext, IMapper mapper) : base(gravityGymDbContext, mapper)
        {

        }

    }
}
