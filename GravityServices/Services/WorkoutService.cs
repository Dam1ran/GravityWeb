using AutoMapper;
using Domain.Entities.WorkoutEntities;
using GravityDAL.Interfaces;
using GravityDTO.WorkoutModels;
using GravityServices.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GravityServices.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IRoutineRepository _routineRepository;
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IMapper _mapper;

        public WorkoutService(
            IRoutineRepository routineRepository,
            IWorkoutRepository workoutRepository,
            IMapper mapper
            )
        {
            _routineRepository = routineRepository;
            _workoutRepository = workoutRepository;
            _mapper = mapper;
        }
        public async Task<WorkoutDTO> CreateWorkoutAsync(WorkoutDTO workoutDTO)
        {
            workoutDTO.EstimatedMin = 60;
            var result = await _workoutRepository.AddAsync(_mapper.Map<Workout>(workoutDTO));
            return _mapper.Map<WorkoutDTO>(result);
        }

        public async Task<WorkoutDTO> UpdateWorkoutDescriptionAsync(WorkoutDTO workoutDTO)
        {
            var workout = await _workoutRepository.GetByIdAsync(workoutDTO.Id);
            workout.WorkoutComments = workoutDTO.WorkoutComments;
            await _workoutRepository.SaveChangesAsync();
            return await GetWorkoutAsync(workoutDTO.Id);
        }

        public async Task<WorkoutDTO> GetWorkoutAsync(long id)
        {            
            return _mapper.Map<WorkoutDTO>(await _workoutRepository.GetByIdAsync(id));
        }

        public async Task<WorkoutDTO> DeleteLastWorkoutFromRoutineAsync(long Id)
        {
            var workout = await _routineRepository.GetLastWorkoutFromRoutineAsync(Id);
            var deletedWorkout = await _workoutRepository.DeleteAsync(workout);
            return _mapper.Map<WorkoutDTO>(deletedWorkout);
        }
    }
}
