using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using GravityDAL.Interfaces;
using GravityServices.Interfaces;
using GravityWeb.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        #region INITIALIZATIONS
        private readonly IWebHostEnvironment _environment;
        private readonly IFileSaver _fileSaver;
        private readonly IGymSessionScheduleRepository _gymSessionScheduleRepository;
        private readonly IUsefulLinksRepository _usefulLinksRepository;
        private readonly IUsefulLinkService _usefulLinkService;

        public InformationController(
            IWebHostEnvironment environment,
            IFileSaver fileSaver,
            IGymSessionScheduleRepository gymSessionScheduleRepository,
            IUsefulLinksRepository usefulLinksRepository,
            IUsefulLinkService usefulLinkService
            )
        {
            _environment = environment;
            _fileSaver = fileSaver;
            _gymSessionScheduleRepository = gymSessionScheduleRepository;
            _usefulLinksRepository = usefulLinksRepository;
            _usefulLinkService = usefulLinkService;
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]FileUploadAPI objfile, [FromForm] DayOfWeek dayOfWeek)
        {

            if (objfile.files.Length > 0)
            {
                var response = await _fileSaver.Save(_environment.WebRootPath, objfile);
                //move to service
                var ziu = new Domain.Entities.GymSessionSchedule { DayOfWeek = dayOfWeek, ImageUrl = response, Description = "description", HourMinute = "08:00" };

                await _gymSessionScheduleRepository.AddAsync(ziu);

                return Ok(new { response });
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get(DayOfWeek dayOfWeek)
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



    }
}