using AutoMapper;
using Domain.Entities;
using GravityDAL.Interfaces;

namespace GravityDAL.Repositories
{
    public class ExerciseTemplateRepository : Repository<ExerciseTemplate>, IExerciseTemplateRepository
    {
        public ExerciseTemplateRepository(GravityGymDbContext gravityGymDbContext, IMapper mapper) : base(gravityGymDbContext, mapper)
        {

        }        
        
    }
}
