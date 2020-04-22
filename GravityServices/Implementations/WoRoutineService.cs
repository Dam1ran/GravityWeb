using AutoMapper;
using Domain.Entities.WorkoutEntities;
using GravityDAL.Interfaces;
using GravityDTO.WORoutine;
using GravityServices.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravityServices.Implementations
{
    public class WoRoutineService : IWoRoutineService
    {
        private IWoRoutineRepository _woRoutineRepository;
        private readonly IWorkoutRepository _workoutRepository;
        private IMapper _mapper;
        private ILogger<WoRoutineService> _logger;


        public WoRoutineService(
            IWoRoutineRepository woRoutineRepository,
            IWorkoutRepository workoutRepository,
            IMapper mapper,
            ILogger<WoRoutineService> logger)
        {
            _woRoutineRepository = woRoutineRepository;
            _workoutRepository = workoutRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<WoRoutineDTO> AddRoutine(WoRoutineDTO wORoutineDTO)
        {            
            var result = await _woRoutineRepository
                .AddAsync(new WoRoutine 
                { 
                    Title= wORoutineDTO.Title,
                    Description = wORoutineDTO.Description                    
                });

            if (result!=null)
            {
                var woRoutineDTO = _mapper.Map<WoRoutineDTO>(result);

                return woRoutineDTO;
            }

            return null;

        }

        public async Task<WorkoutDTO> CreateWorkout(WorkoutDTO workoutDTO)
        {
            try
            {
                workoutDTO.EstimatedMin = 60;
                var result = await _workoutRepository.AddAsync(_mapper.Map<Workout>(workoutDTO));
                return _mapper.Map<WorkoutDTO>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }

        public async Task<bool> DeleteRoutine(long Id)
        {
            var deleted = await _woRoutineRepository.DeleteAsync(Id);

            return deleted != null;
        }

        public async Task<WoRoutineDTO> GetRoutine(long Id)
        {
            var result = await _woRoutineRepository.GetByIdWithInclude(Id, x=>x.Workouts);

            return _mapper.Map<WoRoutineDTO>(result);

        }

        public async Task<IList<WoRoutineNameDTO>> GetRoutines()
        {
            var routines = await _woRoutineRepository.GetAllAsync();

            return routines.Select(x=> new WoRoutineNameDTO { Id=x.Id, Title=x.Title }).ToList();

        }

        public async Task<WorkoutDTO> UpdateWorkoutDescription(WorkoutDTO workoutDTO)
        {
            var routine = await _woRoutineRepository.GetByIdWithInclude(workoutDTO.WoRoutineId, x=>x.Workouts);

            var workout = routine.Workouts                
                .FirstOrDefault(w => w.WoRoutineId == workoutDTO.WoRoutineId && w.Order == workoutDTO.Order);

            workout.WorkoutComments = workoutDTO.WorkoutComments;
            workout.Order = workoutDTO.Order;
            
            try
            {
                await _workoutRepository.SaveChangesAsync();                
                return _mapper.Map<WorkoutDTO>(routine.Workouts.FirstOrDefault(w => w.WoRoutineId == workoutDTO.WoRoutineId && w.Order == workoutDTO.Order));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
    
        }

        public async Task<WorkoutDTO> GetWorkout(long id)
        {
            var workout = await _workoutRepository.GetByIdAsync(id);

            return _mapper.Map<WorkoutDTO>(workout);
        }

        public async Task<bool> DeleteWorkout(long Id)
        {
            var deleted = await _workoutRepository.DeleteAsync(Id);

            return deleted != null;
        }

        public async Task<WorkoutDTO> DeleteLastWorkoutFromRoutine(long Id)
        {
            var routine = await _woRoutineRepository.GetByIdWithInclude(Id, x => x.Workouts);
            if (routine!=null)
            {
                var workout = routine.Workouts.OrderBy(x=>x.Order).LastOrDefault();                
                if (workout!=null)
                {
                    try
                    {
                        var deletedWorkout = await _workoutRepository.DeleteAsync(workout);
                        return _mapper.Map<WorkoutDTO>(deletedWorkout);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        return null;
                    }
                }
            }

            return null;
        }
    }
}
