using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Helpers;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravityServices.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPersonalInfoRepository _personalInfoRepository;
        private readonly IPersonalClientRepository _personalClientRepository;
        private readonly ICoachService _coachService;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IPersonalInfoRepository personalInfoRepository,
            IPersonalClientRepository personalClientRepository,
            ICoachService coachService
            )
        {
            _userManager = userManager;
            _personalInfoRepository = personalInfoRepository;
            _personalClientRepository = personalClientRepository;
            _coachService = coachService;

        }
        public async Task<UserDTOsResponse> GetUsers(string filter,int aPage,int aPageSize)
        {
            var page = aPage < 1 ? 1 : aPage;
            var pageSize = aPageSize < 1 ? 1 : aPageSize;

            var query = _userManager.Users;

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(u => 
                //u.UserName.ToLower().Contains(filter.ToLower()) ||
                u.PersonalInfo.FirstName.ToLower().Contains(filter.ToLower())||
                u.PersonalInfo.LastName.ToLower().Contains(filter.ToLower()) ||
                u.Email.ToLower().Contains(filter.ToLower())
                );
            }


            var users = query
                .Join(_personalInfoRepository.GetUserRoles(),
                u => u.Id,
                r => r.UserId,
                (u, r) => new
                {
                    u.Id,
                    userName = u.UserName,
                    userEmail = u.Email,
                    userRole = r.RoleId,
                    u.PersonalInfo.FirstName,
                    u.PersonalInfo.LastName
                }).GroupJoin(_personalClientRepository.GetPersonalClients(),
                outerKeySelector: q=>q.userEmail,
                innerKeySelector: c=>c.Email,
                resultSelector: (q,c)=> new
                {
                    quer = q,
                    client = c
                }
                ).SelectMany
                (
                    collectionSelector: c=>c.client.DefaultIfEmpty(),
                    resultSelector: (b,c)=> new UserDTO
                    {
                        firstName = b.quer.FirstName,//??"-First Name-",
                        lastName = b.quer.LastName,//?? "-Last Name-",
                        userName = b.quer.userName,
                        userEmail = b.quer.userEmail,
                        userRole = b.quer.userRole,
                        coachId = c.ApplicationUserId
                    }
                ).Distinct().OrderBy(u => u.firstName).ThenBy(u=>u.lastName).ThenBy(u=>u.userEmail);


            var pageOfUserDTO = await users.Page(page: page, pageSize: pageSize).ToListAsync();

            var numOfUsers = users.Count();

            var numberOfPages = pageSize > 1 ? (int)Math.Ceiling((double)numOfUsers / pageSize) : numOfUsers;

            var userDTOsResponse = new UserDTOsResponse { userDTOs=pageOfUserDTO, PageNumber=page,numberOfPages=numberOfPages };
                        
            return userDTOsResponse;
        }

        public async Task<bool> UpdateRole(string userEmail, long roleId)
        {

            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user != null)
            {
                var oldRole = await _userManager.GetRolesAsync(user);

                if (oldRole.SingleOrDefault() == "Coach")
                {
                    await _coachService.RemovePersonalClientsFromCoach(userEmail);
                }

                var removeResult = await _userManager.RemoveFromRoleAsync(user, oldRole.SingleOrDefault());

                if (removeResult.Succeeded)
                {
                    var role = _personalInfoRepository.GetUserRole(roleId);

                    if (role != null)
                    {
                        var addResult = await _userManager.AddToRoleAsync(user, role);

                        if (addResult.Succeeded)
                        {
                            return true;
                        }
                    }

                }

            }

            return false;
        }
    }
}
