using Domain.Entities;
using GravityDAL.DTO;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IPersonalInfoRepository : IRepository<PersonalInfo>
    {
        Task<PersonalInfo> GetPersonalInfoByUserId(long Id);
        string GetUserRole(long roleId);       

    }
}
