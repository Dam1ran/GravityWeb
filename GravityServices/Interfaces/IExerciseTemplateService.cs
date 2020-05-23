using GravityDAL.PageModels;
using GravityDTO;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IExerciseTemplateService
    {
        Task<bool> SaveAsync(ExerciseTemplateDTO exerciseTemplateDTO);
        Task<bool> DeleteAsync(long Id);
        Task<PaginatedResult<ExerciseTemplateDTO>> GetExerciseTemplatesAsync(PaginatedRequest pagedRequest);        
        Task<ExerciseTemplateDTO> GetExerciseTemplateAsync(long Id);
    }
}
