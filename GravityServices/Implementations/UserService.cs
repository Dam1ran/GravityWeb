using Domain.Entities;
using GravityDAL.Interfaces;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace GravityServices.Implementations
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
        
        public async Task<bool> UpdateRole(string userEmail, long roleId)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user != null)
            {
                var oldRole = _userManager.GetRolesAsync(user).Result.SingleOrDefault();
                if (oldRole == "Coach")
                {
                    await _coachService.RemoveAllPersonalClientsFromCoach(userEmail);
                }

                var removeResult = await _userManager.RemoveFromRoleAsync(user, oldRole);
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
