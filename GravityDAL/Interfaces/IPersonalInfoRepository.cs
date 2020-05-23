using Domain.Entities;
using GravityDAL.Models;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IPersonalInfoRepository : IRepository<PersonalInfo>
    {
        Task<PersonalInfo> GetPersonalInfoByUserIdAsync(long Id);
        string GetRoleName(long roleId);
    }
}
