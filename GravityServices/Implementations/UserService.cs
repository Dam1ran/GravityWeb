﻿using Domain.Entities;
using GravityDAL.DTO;
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
