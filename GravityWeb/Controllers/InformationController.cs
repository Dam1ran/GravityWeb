using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Interfaces;
using GravityWeb.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        #region INITIALIZATIONS
        private readonly IWebHostEnvironment _environment;
        private readonly IDayScheduleService _dayScheduleService;
        private readonly IGymSessionScheduleRepository _gymSessionScheduleRepository;
        private readonly IUsefulLinksRepository _usefulLinksRepository;
        private readonly IUsefulLinkService _usefulLinkService;
        private readonly IOurTeamMemberService _ourTeamMemberService;
        private readonly IOurTeamMemberRepository _ourTeamMemberRepository;
        private readonly IGetFBImageUrlsService _getFBImageUrlsService;
        private readonly AppSettings _appsettings;


        public InformationController(
            IWebHostEnvironment environment,
            IDayScheduleService dayScheduleService,
            IGymSessionScheduleRepository gymSessionScheduleRepository,
            IUsefulLinksRepository usefulLinksRepository,
            IUsefulLinkService usefulLinkService,
            IOurTeamMemberService ourTeamMemberService,
            IOurTeamMemberRepository ourTeamMemberRepository,
            IOptions<AppSettings> appSettings,
            IGetFBImageUrlsService getFBImageUrlsService
            )
        {
            _environment = environment;
            _dayScheduleService = dayScheduleService;
            _gymSessionScheduleRepository = gymSessionScheduleRepository;
            _usefulLinksRepository = usefulLinksRepository;
            _usefulLinkService = usefulLinkService;
            _ourTeamMemberService = ourTeamMemberService;
            _ourTeamMemberRepository = ourTeamMemberRepository;
            _getFBImageUrlsService = getFBImageUrlsService;
            _appsettings = appSettings.Value;
        }
        #endregion

        [HttpPost("postschedule")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Post([FromForm]ScheduleUploadData scheduleUploadData)
        {

            if (scheduleUploadData.file.Length > 0)
            {
                var response = await _dayScheduleService.SaveAsync(_environment.WebRootPath, scheduleUploadData);

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
            var listOfActivitiesForRespectiveDay = await _gymSessionScheduleRepository.GetByDayOfWeek(dayOfWeek);
            return Ok(listOfActivitiesForRespectiveDay); 
        }

        
        [HttpGet("links")]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> Links()
        {           

            return Ok(await _usefulLinksRepository.GetAllAsync());

        }

        [HttpDelete("deletelink/{Id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Delete(int Id)
        {
            var link =  await _usefulLinksRepository.GetByIdAsync(Id);
            if (link != null)
            {
                await _usefulLinksRepository.DeleteAsync(link);

                return Ok();
            }

            return BadRequest();

        }

        [HttpPost("postlink")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> PostLink([FromBody]UsefulLink usefulLink)
        {
            var link = await _usefulLinkService.SaveAsync(usefulLink);
            
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
            var teamMember = await _ourTeamMemberService.SaveAsync(_environment.WebRootPath, ourTeamMemberData);

            if (teamMember != null)
            {
                return Ok( teamMember );
            }

            return BadRequest();

        }

        [HttpGet("getteammembers")]
        public async Task<IActionResult> GetTeamMembers()
        {
            var members = await _ourTeamMemberRepository.GetAllAsync();

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