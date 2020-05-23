using Domain.Entities;
using GravityDTO;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalInfoController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPersonalInfoService _personalInfoService;

        private string UserEmail { get => User.FindFirst(ClaimTypes.NameIdentifier)?.Value; }      

        public PersonalInfoController(
            UserManager<ApplicationUser> userManager,
            IPersonalInfoService personalInfoService)
        {
            _userManager = userManager;
            _personalInfoService = personalInfoService;
        }

        [HttpGet]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> Get()
        {            
            var personalInfo = await _personalInfoService.GetByUserIdAsync(GetUserId());
            return Ok(personalInfo);
        }

        /// <summary>
        /// Saves Personal Info
        /// </summary>
        /// <param name="personalInfoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> Post([FromBody]PersonalInfoDTO personalInfoDTO)
        {
            var response = await _personalInfoService.SavePersonalInfoAsync(personalInfoDTO, GetUserId());            
            return Ok(response);
        }

        [NonAction]
        private long GetUserId() => _userManager.FindByEmailAsync(UserEmail).Result.Id;       
        
    }
}