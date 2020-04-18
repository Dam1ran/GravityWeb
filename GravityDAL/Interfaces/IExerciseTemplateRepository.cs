using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IExerciseTemplateRepository : IRepository<ExerciseTemplate>
    {
        Task<IList<ExerciseTemplate>> GetExerciseTemplatesWithInclude();
        IIncludableQueryable<ExerciseTemplate, IList<MuscleExercise>> GetExerciseTemplatesWithIncludeT();
    }
}
