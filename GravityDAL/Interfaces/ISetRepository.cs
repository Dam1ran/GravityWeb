using Domain.Entities.WorkoutEntities;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface ISetRepository : IRepository<Set>
    {
        Task<int> GetNextSetOrderOfExerciseAsync(long exercisesId);
    }
}
