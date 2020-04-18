using GravityDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IExerciseTemplateService
    {
        Task<bool> SaveAsync(ExerciseTemplateDTO exerciseTemplateDTO);
        Task<bool> DeleteAsync(long Id);
        Task<ExerciseTemplateDTOresponse> GetAllETs(ExerciseTemplateRequest exerciseTemplateRequest);
        
    }
}
