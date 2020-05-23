using GravityDTO;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IPersonalInfoService
    {
        Task<PersonalInfoDTO> GetByUserIdAsync(long Id);
        Task<PersonalInfoDTO> SavePersonalInfoAsync(PersonalInfoDTO personalInfoDTO,long UserId);
    }
}
