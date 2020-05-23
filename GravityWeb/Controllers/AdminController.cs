using GravityDAL.PageModels;
using GravityDTO;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly ICoachService _coachService;

        public AdminController(
            IUserService userService,
            IApplicationUserService applicationUserService,
            ICoachService coachService)
        {
            _userService = userService;
            _applicationUserService = applicationUserService;
            _coachService = coachService;
        }

        [HttpPost("getusers")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetUsers([FromBody]PaginatedRequest paginatedRequest)
        {
            var response = await _applicationUserService.GetUsersAsync(paginatedRequest);
            return Ok(response);
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
            await _userService.UpdateRoleAsync(saveUserRoleDTO.userEmail, saveUserRoleDTO.roleId);
            return Ok();
        }

        [HttpPost("assigncoach")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> AssignCoach([FromBody]ActionCoachDTO actionCoachDTO)
        {
            await _coachService.AddPersonalClientAsync(actionCoachDTO.coachEmail, actionCoachDTO.clientEmail);
            return Ok();
        }

        [HttpPost("unassigncoach")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UnassignCoach([FromBody]ActionCoachDTO actionCoachDTO)
        {
            var coach = await _coachService.RemovePersonalClientFromCoachAsync(actionCoachDTO.coachEmail, actionCoachDTO.clientEmail);
            return Ok(coach);
        }
    }
}