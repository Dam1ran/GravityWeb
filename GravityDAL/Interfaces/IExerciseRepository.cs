using Domain.Entities.WorkoutEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        Task<IList<Exercise>> GetByWorkoutIdAsync(long Id);
        Task<Exercise> GetLastWithOrderInferiorToAsync(long workoutId, int order);
        Task<Exercise> GetFirstWithOrderSuperiorToAsync(long workoutId, int order);
    }
}
