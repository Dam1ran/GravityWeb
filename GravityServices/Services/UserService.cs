using Domain.Entities;
using GravityDAL.Interfaces;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GravityServices.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPersonalInfoRepository _personalInfoRepository;
        private readonly ICoachService _coachService;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IPersonalInfoRepository personalInfoRepository,
            ICoachService coachService
            )
        {
            _userManager = userManager;
            _personalInfoRepository = personalInfoRepository;
            _coachService = coachService;

        }
        
        public async Task<bool> UpdateRoleAsync(string userEmail, long roleId)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            
            var oldRole = _userManager.GetRolesAsync(user).Result.SingleOrDefault();
            if (oldRole == "Coach")
            {
                await _coachService.RemoveAllPersonalClientsFromCoachAsync(userEmail);
            }

            if (_userManager.RemoveFromRoleAsync(user, oldRole).Result.Succeeded)
            {
                var role = _personalInfoRepository.GetRoleName(roleId);
                return _userManager.AddToRoleAsync(user, role).Result.Succeeded;
            }

            throw new Exception($"Wrong Role Id");
        }
    }
}
