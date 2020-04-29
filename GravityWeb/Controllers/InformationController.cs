using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Interfaces;
using GravityWeb.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        #region INITIALIZATIONS
        private readonly IWebHostEnvironment _environment;
        private readonly IGymSessionScheduleService _gymSessionScheduleService;
        private readonly IGymSessionScheduleRepository _gymSessionScheduleRepository;
        private readonly IUsefulLinksRepository _usefulLinksRepository;
        private readonly IOurTeamMemberService _ourTeamMemberService;
        private readonly IOurTeamMemberRepository _ourTeamMemberRepository;
        private readonly IGetFBImageUrlsService _getFBImageUrlsService;
        private readonly ILogger<InformationController> _logger;
        private readonly AppSettings _appsettings;


        public InformationController(
            IWebHostEnvironment environment,
            IGymSessionScheduleService gymSessionScheduleService,
            IGymSessionScheduleRepository gymSessionScheduleRepository,
            IUsefulLinksRepository usefulLinksRepository,
            IOurTeamMemberService ourTeamMemberService,
            IOurTeamMemberRepository ourTeamMemberRepository,
            IOptions<AppSettings> appSettings,
            IGetFBImageUrlsService getFBImageUrlsService,
            ILogger<InformationController> logger
            )
        {
            _environment = environment;
            _gymSessionScheduleService = gymSessionScheduleService;
            _gymSessionScheduleRepository = gymSessionScheduleRepository;
            _usefulLinksRepository = usefulLinksRepository;
            _ourTeamMemberService = ourTeamMemberService;
            _ourTeamMemberRepository = ourTeamMemberRepository;
            _getFBImageUrlsService = getFBImageUrlsService;
            _appsettings = appSettings.Value;
            _logger = logger;
        }
        #endregion

        [HttpPost("postschedule")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Post([FromForm]ScheduleUploadData scheduleUploadData)
        {

            if (scheduleUploadData.file.Length > 0)
            {
                var response = await _gymSessionScheduleService.SaveAsync(_environment.WebRootPath,_appsettings.UploadScheduleFolderName, scheduleUploadData);

                if (response!=null)
                {
                    return Ok( response );
                }

            }

            return BadRequest();
        }

        [HttpDelete("deleteSchedule/{Id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteSchedule(int Id)
        {
            var schedule = await _gymSessionScheduleRepository.GetByIdAsync(Id);

            if (schedule != null)
            {
                await _gymSessionScheduleRepository.DeleteAsync(schedule);

                return Ok();
            }            

            return BadRequest();
        }

        [HttpGet("scheduleforday/{dayOfWeek}")]
        public async Task<IActionResult> Get(string dayOfWeek)
        {                     
            return Ok(await _gymSessionScheduleService.GetByDayOfWeek(dayOfWeek, _appsettings.BaseUrl, _appsettings.UploadScheduleFolderName)); 
        }

        
        [HttpGet("links")]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> Links()
        {           
            return Ok(await _usefulLinksRepository.GetAllAsync());
        }

        [HttpDelete("deletelink/{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _usefulLinksRepository.DeleteAsync(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

        }

        [HttpPost("postlink")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> PostLink([FromBody]UsefulLink usefulLink)
        {
            var link = await _usefulLinksRepository.AddAsync(usefulLink);
            
            if (link != null)
            {
                return Ok();
            }
                       
            return BadRequest();
            
        }

        [HttpPost("postteammember")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> PostTeamMember([FromForm]OurTeamMemberData ourTeamMemberData)
        {
            var teamMember = await _ourTeamMemberService.SaveAsync(_environment.WebRootPath, _appsettings.UploadTeamMemberFolderName, ourTeamMemberData);

            if (teamMember != null)
            {
                return Ok( teamMember );
            }

            return BadRequest();

        }

        [HttpGet("getteammembers")]
        public async Task<IActionResult> GetTeamMembers()
        {
            var members = await _ourTeamMemberService.GetAllAsync(_appsettings.BaseUrl, _appsettings.UploadTeamMemberFolderName);

            return Ok(members);
        }

        [HttpDelete("deleteteammember/{Id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteTeamMember(int Id)
        {
            var member = await _ourTeamMemberRepository.GetByIdAsync(Id);

            if (member != null)
            {
                await _ourTeamMemberRepository.DeleteAsync(member);

                return Ok();
            }

            return BadRequest();

        }

        [HttpPost("gallerypage")]
        public async Task<IActionResult> GalleryPage([FromForm]string nav)
        {
            var galImagesUrlDto = await _getFBImageUrlsService.GetImagesUrl(nav,_appsettings.FBAccessToken);

            if (galImagesUrlDto!=null)
            {
                return Ok(galImagesUrlDto);
            }

            return BadRequest();

        }


    }
}