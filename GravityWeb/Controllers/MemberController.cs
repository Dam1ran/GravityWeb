using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Interfaces;
using GravityWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AppSettings _appsettings;
        private ITeamMemberService _teamMemberService;
        private ITeamMemberRepository _teamMemberRepository;

        public MemberController(
            IWebHostEnvironment environment,
            IOptions<AppSettings> appSettings,
            ITeamMemberService teamMemberService,
            ITeamMemberRepository teamMemberRepository)
        {
            _environment = environment;
            _appsettings = appSettings.Value;
            _teamMemberService = teamMemberService;
            _teamMemberRepository = teamMemberRepository;
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Post([FromForm]TeamMemberData teamMemberData)
        {
            var teamMember = await _teamMemberService.SaveAsync(_environment.WebRootPath, _appsettings.UploadTeamMemberFolderName, teamMemberData);
            return Ok(teamMember);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var members = await _teamMemberService.GetAllAsync(_appsettings.BaseUrl, _appsettings.UploadTeamMemberFolderName);
            return Ok(members);
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _teamMemberRepository.DeleteAsync(Id);
            return Ok();            
        }
    }
}