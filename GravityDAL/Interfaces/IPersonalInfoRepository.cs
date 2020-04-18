using Domain.Auth;
using Domain.Entities;
using GravityDAL.DTO;
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
        string GetUserRole(long roleId);
        Task<AppUserDTOsResponse> GetUsers(string filter, int aPage, int aPageSize);

    }
}
