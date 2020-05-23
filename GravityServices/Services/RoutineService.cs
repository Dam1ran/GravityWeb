using AutoMapper;
using Domain.Entities.WorkoutEntities;
using GravityDAL.Interfaces;
using GravityDTO.WorkoutModels;
using GravityServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravityServices.Services
{
    public class RoutineService : IRoutineService
    {
        private IRoutineRepository _routineRepository;        
        private IMapper _mapper;

        public RoutineService(
            IRoutineRepository routineRepository,
            IMapper mapper)
        {
            _routineRepository = routineRepository;
            _mapper = mapper;  
        }

        public async Task<RoutineDTO> AddRoutineAsync(RoutineDTO routineDTO)
        {
            var result = await _routineRepository
                .AddAsync(new Routine { Title = routineDTO.Title, Description = routineDTO.Description });

            return _mapper.Map<RoutineDTO>(result);

        }

        public async Task<bool> DeleteRoutineAsync(long Id)
        {
            var deleted = await _routineRepository.DeleteAsync(Id);
            return deleted != null;
        }

        public async Task<RoutineDTO> GetRoutineAsync(long Id)
        {
            var result = await _routineRepository.GetByIdWithIncludeAsync(Id, x => x.Workouts);
            return _mapper.Map<RoutineDTO>(result);
        }

        public async Task<IList<RoutineNameDTO>> GetRoutinesNameAsync()
        {
            var routines = await _routineRepository.GetAllAsync();
            return routines.Select(x => new RoutineNameDTO { Id = x.Id, Title = x.Title }).ToList();
        }
        
    }


}
