using Domain.Entities;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravityDAL.Implementations
{
    public class ExerciseTemplateRepository : Repository<ExerciseTemplate>, IExerciseTemplateRepository
    {
        public ExerciseTemplateRepository(GravityGymDbContext gravityGymDbContext) : base(gravityGymDbContext)
        {

        }

        public async Task<IList<ExerciseTemplate>> GetExerciseTemplatesWithInclude()
        {
            return await _dbSet.Include(x => x.MuscleExercises).ToListAsync();
        }

        public IIncludableQueryable<ExerciseTemplate,IList<MuscleExercise>> GetExerciseTemplatesWithIncludeT()
        {            
            return _dbSet.Include(x => x.MuscleExercises);
        }
    }
}
