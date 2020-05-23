using GravityDTO.WorkoutModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IRoutineService
    {
        Task<RoutineDTO> AddRoutineAsync(RoutineDTO routineDTO);
        Task<bool> DeleteRoutineAsync(long Id);        
        Task<IList<RoutineNameDTO>> GetRoutinesNameAsync();
        Task<RoutineDTO> GetRoutineAsync(long Id);
    }
}
