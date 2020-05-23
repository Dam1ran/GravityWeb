using GravityDTO.WorkoutModels;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface ISetService
    {
        Task<SetDTO> AddSetAsync(SetDTO setDTO);
        Task<bool> DeleteSetAsync(long Id);
    }
}
