using GravityDTO.WORoutine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IWoRoutineService
    {
        Task<WoRoutineDTO> AddRoutine(WoRoutineDTO wORoutineDTO);
        Task<bool> DeleteRoutine(long Id);
        Task<bool> DeleteWorkout(long Id);
        Task<WorkoutDTO> DeleteLastWorkoutFromRoutine(long Id);
        Task<WorkoutDTO> CreateWorkout(WorkoutDTO workoutDTO);
        Task<WorkoutDTO> GetWorkout(long id);
        Task<WorkoutDTO> UpdateWorkoutDescription(WorkoutDTO workoutDTO);
        Task<IList<WoRoutineNameDTO>> GetRoutines();
        Task<WoRoutineDTO> GetRoutine(long Id);
        Task<IList<ExerciseDTO>> AddExerciseToWorkout(ExerciseDTO exerciseDTO);
        Task<IList<ExerciseDTO>> GetExercisesFromWorkout(long id);
        Task<bool> DeleteExercise(long Id);
        Task<IList<ExerciseDTO>> Swap(ExerciseDTO exerciseDTO, bool upDown);

    }
}
