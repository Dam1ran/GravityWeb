using GravityDAL.PageModels;
using GravityDTO;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IExerciseTemplateService
    {
        Task<bool> SaveAsync(ExerciseTemplateDTO exerciseTemplateDTO);
        Task<bool> DeleteAsync(long Id);
        Task<PaginatedResult<ExerciseTemplateDTO>> GetExerciseTemplates(PaginatedRequest pagedRequest);        
        Task<ExerciseTemplateDTO> GetExerciseTemplate(long Id);
    }
}
