using Domain.Entities.WorkoutEntities;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IRoutineRepository : IRepository<Routine>
    {
        Task<Workout> GetLastWorkoutFromRoutineAsync(long routineId);
    }
}
