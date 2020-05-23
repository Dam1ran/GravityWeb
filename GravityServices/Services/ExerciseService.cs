using AutoMapper;
using Domain.Entities.WorkoutEntities;
using GravityDAL.Interfaces;
using GravityDTO.WorkoutModels;
using GravityServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravityServices.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _exerciseRepository;
        public ExerciseService(
            IExerciseRepository exerciseRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<IList<ExerciseDTO>> AddExerciseToWorkoutAsync(ExerciseDTO exerciseDTO)
        {
            var existentExercises = await _exerciseRepository.GetByWorkoutIdAsync(exerciseDTO.WorkoutId);
            var order = -1;
            if (existentExercises.Count > 0)
            {
                order = existentExercises.Select(x => x.Order).LastOrDefault();
            }

            var exerciseToAdd = new Exercise { Order = ++order, ExerciseTemplateId = exerciseDTO.ExerciseTemplateId, WorkoutId = exerciseDTO.WorkoutId };

            await _exerciseRepository.AddAsync(exerciseToAdd);
            return await GetExercisesFromWorkoutAsync(exerciseDTO.WorkoutId);
        }

        public async Task<IList<ExerciseDTO>> GetExercisesFromWorkoutAsync(long id)
        {
            var exercises = await _exerciseRepository.GetByWorkoutIdAsync(id);
            return _mapper.Map<List<ExerciseDTO>>(exercises);
        }

        public async Task<bool> DeleteExerciseAsync(long Id)
        {
            var deleted = await _exerciseRepository.DeleteAsync(Id);
            return deleted != null;
        }

        public async Task<IList<ExerciseDTO>> SwapAsync(ExerciseDTO exerciseDTO, bool upDown)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(exerciseDTO.Id);
            if (exercise == null)
            {
                throw new Exception($"Object of type {typeof(Exercise).ToString().Split('.').Last()} with id { exerciseDTO.Id } not found");
            }

            var exerciseToSwapOrder = upDown ?
                await _exerciseRepository.GetLastWithOrderInferiorToAsync(exercise.WorkoutId, exercise.Order) :
                await _exerciseRepository.GetFirstWithOrderSuperiorToAsync(exercise.WorkoutId, exercise.Order);

            if (exerciseToSwapOrder == null)
            {
                throw new Exception($"Cannot swap with non existent {typeof(Exercise).ToString().Split('.').Last()}");
            }

            int order = exercise.Order;
            exercise.Order = exerciseToSwapOrder.Order;
            exerciseToSwapOrder.Order = order;

            await _exerciseRepository.SaveChangesAsync();

            return await GetExercisesFromWorkoutAsync(exercise.WorkoutId);
            
        }
    }
}
