using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinetController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPersonalInfoService _personalInfoService;
        private readonly IUserService _userService;
        private readonly ICoachService _coachService;
        private readonly IPersonalInfoRepository _personalInfoRepository;

        public CabinetController(
            UserManager<ApplicationUser> userManager,
            IPersonalInfoService personalInfoService,
            IUserService userService,
            ICoachService coachService,
            IPersonalInfoRepository personalInfoRepository
            )
        {
            _userManager = userManager;
            _personalInfoService = personalInfoService;
            _userService = userService;
            _coachService = coachService;
            _personalInfoRepository = personalInfoRepository;
        }

        [HttpGet("getpersonalinfo")]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> GetPersonalInfo()
        {           
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            if (user!=null)
            {
                var personalInfo = await _personalInfoService.GetByUserId(user.Id);

                return Ok( personalInfo );
            }

            return BadRequest();
        }

        [HttpPost("savepersonalinfo")]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> SavePersonalInfo([FromBody]PersonalInfoDTO personalInfoDTO)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            if (user!=null)
            {
                var response = await _personalInfoService.SavePersonalInfo(personalInfoDTO,user.Id);

                return Ok(response);
            }

            return BadRequest();
        
        }

        [HttpPost("getusers")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetUsers([FromBody]GetUserRequestDTO getUserRequestDTO)
        {           
            var userDTOsResponse = await _userService
            .GetUsers(
                    getUserRequestDTO.filter,
                    getUserRequestDTO.page,
                    getUserRequestDTO.pageSize
                );

            return Ok(userDTOsResponse);
            
        }

        [HttpGet("getcoaches")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetCoaches()
        {
            
            var coachDTOs = await _coachService.GetCoachesAsync();
            
            return Ok(coachDTOs);

        }

        [HttpPost("saveuserrole")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> SaveUserRole([FromBody]SaveUserRoleDTO saveUserRoleDTO)
        {            

            var result = await _userService.UpdateRole(saveUserRoleDTO.userEmail, saveUserRoleDTO.roleId);

            if (result)
            {
                return Ok();
            }

            return BadRequest();

        }

        [HttpPost("assigncoach")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> AssignCoach([FromBody]ActionCoachDTO actionCoachDTO)
        {

            var user = await _coachService.AddPersonalClient(actionCoachDTO.coachEmail, actionCoachDTO.clientEmail);                       

            if (user!=null)
            {
                return Ok();
            }

            return BadRequest();

        }        

        [HttpPost("unassigncoach")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UnassignCoach([FromBody]ActionCoachDTO actionCoachDTO)
        {

            var user = await _coachService.RemovePersonalClientsFromCoach(actionCoachDTO.coachEmail,actionCoachDTO.clientEmail);

            if (user != null)
            {
                return Ok();
            }

            return BadRequest();

        }

    }
}