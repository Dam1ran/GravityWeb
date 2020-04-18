using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GravityServices.Helpers;
using Microsoft.EntityFrameworkCore;

namespace GravityServices.Implementations
{
    public class ExerciseTemplateService : IExerciseTemplateService
    {
        private IMuscleRepository _muscleRepository;
        private IExerciseTemplateRepository _exerciseTemplateRepository;
        private IMuscleExerciseRepository _muscleExerciseRepository;
        private ILogger<ExerciseTemplateService> _logger;

        public ExerciseTemplateService(
            IExerciseTemplateRepository exerciseTemplateRepository,
            IMuscleExerciseRepository muscleExerciseRepository,
            IMuscleRepository muscleRepository,
            ILogger<ExerciseTemplateService> logger)
        {
            _muscleRepository = muscleRepository;
            _exerciseTemplateRepository = exerciseTemplateRepository;
            _muscleExerciseRepository = muscleExerciseRepository;
            _logger = logger;
        }

        public async Task<bool> DeleteAsync(long Id)
        {
            var deleted = await _exerciseTemplateRepository.DeleteAsync(Id);

            if (deleted != null)
            {
                return true;
            }

            return false;
        }

        public async Task<ExerciseTemplateDTOresponse> GetAllETs(ExerciseTemplateRequest exerciseTemplateRequest)
        {            
            //var muscleExercises = await _muscleExerciseRepository.GetAllAsync();

            var page = exerciseTemplateRequest.Page < 1 ? 1 : exerciseTemplateRequest.Page;
            var pageSize = exerciseTemplateRequest.PageSize < 1 ? 1 : exerciseTemplateRequest.PageSize;

            //var exerciseTemplates = await _exerciseTemplateRepository.GetExerciseTemplatesWithInclude();
            var exerciseTemplates = _exerciseTemplateRepository.GetExerciseTemplatesWithIncludeT().OrderBy(et=>et.Name);

            if (!string.IsNullOrEmpty(exerciseTemplateRequest.Filter))
            {
                exerciseTemplates = exerciseTemplates.Where(et =>
                    et.Name.ToLower().Contains(exerciseTemplateRequest.Filter.ToLower())
                ).OrderBy(et => et.Name);
            }

            var length = exerciseTemplates.Count();

            var pageOfEtDTO = await exerciseTemplates.Page(page: page, pageSize: pageSize).ToListAsync();


            var exerciseTemplateDTOs = pageOfEtDTO.Select(et=> new ExerciseTemplateDTO 
            {
                Id = et.Id,
                Name = et.Name,
                Comments = et.Comments,
                Tempo = et.Tempo,
                PrimaryMuscleId = et.MuscleExercises.ElementAtOrDefault(0) != null ? et.MuscleExercises.ElementAtOrDefault(0).MuscleId : 0L,
                SecondaryMuscleId = et.MuscleExercises.ElementAtOrDefault(1) != null ? et.MuscleExercises.ElementAtOrDefault(1).MuscleId : 0L,
            }).OrderBy(x=>x.Name).ToList();


            var response = new ExerciseTemplateDTOresponse { ExerciseTemplateDTOs = exerciseTemplateDTOs, PageSize = pageSize, Length = length, PageIndex = page };


            return response;
        }

        public async Task<bool> SaveAsync(ExerciseTemplateDTO exerciseTemplateDTO)
        {

            var et = new ExerciseTemplate
            {
                Name = exerciseTemplateDTO.Name,
                Comments = exerciseTemplateDTO.Comments,
                Tempo = exerciseTemplateDTO.Tempo
            };

            long etId = 0;

            try
            {
                etId = _exerciseTemplateRepository.AddAsync(et).Result.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                
                return false;
            }


            if (exerciseTemplateDTO.PrimaryMuscleId > 0)
            {
                if (!AddMuscleToExercise(exerciseTemplateDTO.PrimaryMuscleId, etId).Result)
                {
                    await _exerciseTemplateRepository.DeleteAsync(etId);
                    return false;
                }
            }

            if (exerciseTemplateDTO.SecondaryMuscleId > 0)
            {
                if (!AddMuscleToExercise(exerciseTemplateDTO.SecondaryMuscleId, etId).Result)
                {
                    await _exerciseTemplateRepository.DeleteAsync(etId);
                    return false;
                }
            }

            return true;           

        }

        private async Task<bool> AddMuscleToExercise(long muscleId,long exTplId)
        {            
            var muscle = await _muscleRepository.GetByIdAsync(muscleId);

            if (muscle!=null)
            {
                try
                {
                    await _muscleExerciseRepository.AddAsync(new MuscleExercise { MuscleId = muscle.Id, ExerciseTemplateId = exTplId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);                    
                    return false;
                }

                return true;

            }

            return false;

        }

    }
}
