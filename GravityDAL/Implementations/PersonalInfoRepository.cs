using Domain.Auth;
using Domain.Entities;
using GravityDAL.DTO;
using GravityDAL.Helpers;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GravityDAL.Implementations
{
    public class PersonalInfoRepository : Repository<PersonalInfo>, IPersonalInfoRepository
    {
        private readonly DbSet<UserRole> _userRoles;
        private readonly DbSet<Role> _roles;
        private readonly DbSet<ApplicationUser> _users;
        private readonly DbSet<PersonalClient> _personalClients;

        public PersonalInfoRepository(GravityGymDbContext gravityGymDbContext) : base(gravityGymDbContext)
        {
            _userRoles = gravityGymDbContext.UserRoles;
            _roles = gravityGymDbContext.Roles;
            _users = gravityGymDbContext.Users;
            _personalClients = gravityGymDbContext.PersonalClients;
            
        }

        public async Task<PersonalInfo> GetPersonalInfoByUserId(long Id)
        {
            var personalInfo = await _dbSet.Where(pi => pi.ApplicationUserId == Id).FirstOrDefaultAsync();

            return personalInfo;
        }
              
        public IQueryable<UserRole> GetUserRoles()
        {
            return _userRoles;
        }

        public string GetUserRole(long roleId)
        {
            return _roles.Where(r=>r.Id==roleId).Select(rs=>rs.Name).First();
        }

        public async Task<AppUserDTOsResponse> GetUsers(string filter, int aPage, int aPageSize)
        {
            var page = aPage < 1 ? 1 : aPage;
            var pageSize = aPageSize < 1 ? 1 : aPageSize;
                                                  
            IQueryable<ApplicationUser> query = _users
                .Include(u=>u.PersonalInfo)
                .Include(u=>u.Clients)
                .Include(u=>u.Roles);

            //var ziu = _users.Where(u => u.Id == 2L).Include(u => u.Roles).FirstOrDefault();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(u =>
                u.PersonalInfo.FirstName.ToLower().Contains(filter.ToLower()) ||
                u.PersonalInfo.LastName.ToLower().Contains(filter.ToLower()) ||
                u.Email.ToLower().Contains(filter.ToLower())
                );
            }

            var pageOfAppUsers = query
                .OrderBy(u => u.PersonalInfo.LastName)
                .ThenBy(u => u.PersonalInfo.FirstName)
                .ThenBy(u => u.Email)
                .Page(page: page, pageSize: pageSize);

            var appUserDTOs = await pageOfAppUsers.Select(au => new AppUserDTO
            {
                userName = au.UserName,
                firstName = au.PersonalInfo.FirstName,
                lastName = au.PersonalInfo.LastName,
                userEmail = au.Email,
                userRoleId = au.Roles.FirstOrDefault().RoleId,
                coachId = _personalClients
                .Where(pc => pc.Email == au.Email).FirstOrDefault() != null ? _personalClients.Where(pc => pc.Email == au.Email).First().ApplicationUserId : 0
            }
            ).ToListAsync();

            //var appUserDTOs = await pageOfAppUsers.Join(_userRoles,
            //    u => u.Id,
            //    r => r.UserId,
            //    (u,r)=> new AppUserDTO
            //    {
            //        userName = u.UserName,
            //        firstName = u.PersonalInfo.FirstName,
            //        lastName = u.PersonalInfo.LastName,
            //        userEmail = u.Email,
            //        userRoleId = r.RoleId,                    
            //        coachId = _personalClients
            //        .Where(pc=>pc.Email==u.Email).FirstOrDefault()!=null ? _personalClients.Where(pc => pc.Email == u.Email).First().ApplicationUserId : 0
            //    }
            //    ).ToListAsync();

            var numOfUsers = query.Count();

            var numberOfPages = pageSize > 1 ? (int)Math.Ceiling((double)numOfUsers / pageSize) : numOfUsers;

            var appUserDTOsResponse = new AppUserDTOsResponse { appUserDTOs = appUserDTOs, PageNumber = page, numberOfPages = numberOfPages };
            
            return appUserDTOsResponse;
        }
    }
}
