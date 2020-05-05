using Domain.Entities.WorkoutEntities;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IExerciseSetRepository : IRepository<ExerciseSet>
    {
        Task<int> GetNextSetOrderOfExercise(long exercisesId);
    }
}
