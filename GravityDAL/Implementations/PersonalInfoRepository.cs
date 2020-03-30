using Domain.Auth;
using Domain.Entities;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravityDAL.Implementations
{
    public class PersonalInfoRepository : Repository<PersonalInfo>, IPersonalInfoRepository
    {
        private readonly DbSet<UserRole> _userRoles;
        private readonly DbSet<PersonalInfo> _personalInfos;
        private readonly DbSet<Role> _roles;

        public PersonalInfoRepository(GravityGymDbContext gravityGymDbContext) : base(gravityGymDbContext)
        {
            _userRoles = gravityGymDbContext.UserRoles;
            _personalInfos = gravityGymDbContext.PersonalInfos;
            _roles = gravityGymDbContext.Roles;
        }

        public async Task<PersonalInfo> GetPersonalInfoByUserId(long Id)
        {
            var personalInfo = await _dbSet.Where(pi=>pi.ApplicationUserId==Id).FirstOrDefaultAsync();
            
            return personalInfo;
        }
              
        public IQueryable<UserRole> GetUserRoles()
        {
            return _userRoles;
        }
        public IQueryable<PersonalInfo> GetPersonalInfos()
        {
            return _personalInfos;
        }

        public string GetUserRole(long roleId)
        {
            return _roles.Where(r=>r.Id==roleId).Select(rs=>rs.Name).First();
        }
    }
}
