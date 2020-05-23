using GravityDTO.WorkoutModels;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IWorkoutService
    {
        Task<WorkoutDTO> CreateWorkoutAsync(WorkoutDTO workoutDTO);
        Task<WorkoutDTO> GetWorkoutAsync(long id);
        Task<WorkoutDTO> UpdateWorkoutDescriptionAsync(WorkoutDTO workoutDTO);
        Task<WorkoutDTO> DeleteLastWorkoutFromRoutineAsync(long Id);

    }
}
