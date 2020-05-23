using AutoMapper;
using Domain.Entities.WorkoutEntities;
using GravityDAL.Interfaces;
using GravityDTO.WorkoutModels;
using GravityServices.Interfaces;
using System.Threading.Tasks;

namespace GravityServices.Services
{
    public class SetService : ISetService
    {
        private readonly ISetRepository _setRepository;
        private readonly IMapper _mapper;

        public SetService(
            ISetRepository setRepository,
            IMapper mapper)
        {
            _setRepository = setRepository;
            _mapper = mapper;
        }

        public async Task<SetDTO> AddSetAsync(SetDTO setDTO)
        {
            var set = _mapper.Map<Set>(setDTO);
            set.Order = await _setRepository.GetNextSetOrderOfExerciseAsync(setDTO.ExerciseId);            
            return _mapper.Map<SetDTO>(await _setRepository.AddAsync(set));
        }

        public async Task<bool> DeleteSetAsync(long Id)
        {
            var deleted = await _setRepository.DeleteAsync(Id);
            return deleted != null;
        }
    }
}
