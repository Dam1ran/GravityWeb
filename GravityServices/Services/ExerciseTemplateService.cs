using AutoMapper;
using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDAL.PageModels;
using GravityDTO;
using GravityServices.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GravityServices.Services
{
    public class ExerciseTemplateService : IExerciseTemplateService
    {
        private IExerciseTemplateRepository _exerciseTemplateRepository;
        private IMapper _mapper;

        public ExerciseTemplateService(
            IExerciseTemplateRepository exerciseTemplateRepository,
            IMapper mapper)
        {
            _exerciseTemplateRepository = exerciseTemplateRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(long Id)
        {
            var deleted = await _exerciseTemplateRepository.DeleteAsync(Id);
            return deleted != null;
        }

        public async Task<ExerciseTemplateDTO> GetExerciseTemplateAsync(long Id)
        {
            var exerciseTemplate = await _exerciseTemplateRepository.GetByIdWithIncludeAsync(Id, x=>x.PrimaryMuscle, x=>x.SecondaryMuscle);
            return _mapper.Map<ExerciseTemplateDTO>(exerciseTemplate);            
        }

        public async Task<PaginatedResult<ExerciseTemplateDTO>> GetExerciseTemplatesAsync(PaginatedRequest paginatedRequest)
        {
            return await _exerciseTemplateRepository.GetPagedDataAsync<ExerciseTemplateDTO>(paginatedRequest);
        }

        public async Task<bool> SaveAsync(ExerciseTemplateDTO exerciseTemplateDTO)
        {
            ExerciseTemplate exerciseTemplate = null;

            if (exerciseTemplateDTO.Id >= 1)
            {
                exerciseTemplate = await _exerciseTemplateRepository.UpdateAsync(_mapper.Map<ExerciseTemplate>(exerciseTemplateDTO));
            }
            else
            {
                exerciseTemplate = await _exerciseTemplateRepository.AddAsync(_mapper.Map<ExerciseTemplate>(exerciseTemplateDTO));
            }

            return exerciseTemplate != null;
        }

    }
}
