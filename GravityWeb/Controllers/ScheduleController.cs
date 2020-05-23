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
    public class ScheduleController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AppSettings _appsettings;
        private readonly IGymSessionScheduleService _gymSessionScheduleService;
        private readonly IGymSessionScheduleRepository _gymSessionScheduleRepository;
        public ScheduleController(
            IWebHostEnvironment environment,
            IOptions<AppSettings> appSettings,
            IGymSessionScheduleService gymSessionScheduleService,
            IGymSessionScheduleRepository gymSessionScheduleRepository
            )
        {
            _environment = environment;
            _appsettings = appSettings.Value;
            _gymSessionScheduleService = gymSessionScheduleService;
            _gymSessionScheduleRepository = gymSessionScheduleRepository;
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Post([FromForm]ScheduleUploadData scheduleUploadData)
        {
            if (scheduleUploadData.file.Length > 0)
            {
                var response = await _gymSessionScheduleService.SaveAsync(_environment.WebRootPath, _appsettings.UploadScheduleFolderName, scheduleUploadData);
                return Ok(response);
            }

            return BadRequest("Wrong File Length");
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteSchedule(int Id)
        {
            await _gymSessionScheduleRepository.DeleteAsync(Id);
            return Ok();
        }

        [HttpGet("scheduleforday/{dayOfWeek}")]
        public async Task<IActionResult> GetForDay(string dayOfWeek)
        {
            return Ok(await _gymSessionScheduleService.GetByDayOfWeekAsync(dayOfWeek, _appsettings.BaseUrl, _appsettings.UploadScheduleFolderName));
        }
    }
}