using Domain.Entities.WorkoutEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        Task<IList<Exercise>> GetByWorkoutId(long Id);
        Task<Exercise> GetLastWithOrderInferiorTo(long workoutId, int order);
        Task<Exercise> GetFirstWithOrderSuperiorTo(long workoutId, int order);
    }
}
