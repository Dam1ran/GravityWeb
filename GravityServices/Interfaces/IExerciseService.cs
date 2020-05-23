using GravityDTO.WorkoutModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IExerciseService
    {
        Task<IList<ExerciseDTO>> AddExerciseToWorkoutAsync(ExerciseDTO exerciseDTO);
        Task<IList<ExerciseDTO>> GetExercisesFromWorkoutAsync(long id);
        Task<bool> DeleteExerciseAsync(long Id);
        Task<IList<ExerciseDTO>> SwapAsync(ExerciseDTO exerciseDTO, bool upDown);
    }
}
