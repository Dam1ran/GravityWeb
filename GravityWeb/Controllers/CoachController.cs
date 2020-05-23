using GravityServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly ICoachService _coachService;
        public string UserEmail { get => User.FindFirst(ClaimTypes.NameIdentifier)?.Value; }

        public CoachController(ICoachService coachService)
        {
            _coachService = coachService;
        }

        [HttpGet("getmyclients")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> GetMyClients()
        {
            var clients = await _coachService.GetPersonalClientsAsync(UserEmail);
            return Ok(clients);
        }
    }
}