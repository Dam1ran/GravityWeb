using AutoMapper;
using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDAL.PageModels;
using GravityDTO;
using GravityServices.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GravityServices.Implementations
{
    public class ExerciseTemplateService : IExerciseTemplateService
    {
        private IExerciseTemplateRepository _exerciseTemplateRepository;
        private ILogger<ExerciseTemplateService> _logger;
        private IMapper _mapper;

        public ExerciseTemplateService(
            IExerciseTemplateRepository exerciseTemplateRepository,
            IMapper mapper,
            ILogger<ExerciseTemplateService> logger)
        {
            _exerciseTemplateRepository = exerciseTemplateRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(long Id)
        {
            var deleted = await _exerciseTemplateRepository.DeleteAsync(Id);

            return deleted != null;
        }

        public async Task<PaginatedResult<ExerciseTemplateDTO>> GetExerciseTemplates(PaginatedRequest paginatedRequest)
        {
            var result = await _exerciseTemplateRepository.GetPagedData<ExerciseTemplateDTO>(paginatedRequest);

            return result;
        }

        public async Task<bool> SaveAsync(ExerciseTemplateDTO exerciseTemplateDTO)
        {
            try
            {
                if (exerciseTemplateDTO.Id >= 1)
                {
                    await _exerciseTemplateRepository.UpdateAsync(_mapper.Map<ExerciseTemplate>(exerciseTemplateDTO));
                }
                else
                {
                    if (exerciseTemplateDTO.PrimaryMuscleId==0)
                    {
                        exerciseTemplateDTO.PrimaryMuscleId = null;
                    }
                    if (exerciseTemplateDTO.SecondaryMuscleId == 0)
                    {
                        exerciseTemplateDTO.SecondaryMuscleId = null;
                    }
                    await _exerciseTemplateRepository.AddAsync(_mapper.Map<ExerciseTemplate>(exerciseTemplateDTO));
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                
                return false;
            }
        }

    }
}
