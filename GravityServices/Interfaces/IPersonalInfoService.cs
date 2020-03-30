using GravityDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IPersonalInfoService
    {
        Task<PersonalInfoDTO> GetByUserId(long Id);
        Task<PersonalInfoDTO> SavePersonalInfo(PersonalInfoDTO personalInfoDTO,long UserId);
    }
}
