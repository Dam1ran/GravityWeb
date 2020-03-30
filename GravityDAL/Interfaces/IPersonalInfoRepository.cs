using Domain.Auth;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IPersonalInfoRepository : IRepository<PersonalInfo>
    {
        Task<PersonalInfo> GetPersonalInfoByUserId(long Id);
        IQueryable<UserRole> GetUserRoles();
        IQueryable<PersonalInfo> GetPersonalInfos();
        string GetUserRole(long roleId);
        
    }
}
