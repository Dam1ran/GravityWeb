using GravityDTO.WorkoutModels;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetController : ControllerBase
    {
        private readonly ISetService _setService;
        public SetController(ISetService setService)
        {
            _setService = setService;
        }

        [HttpPost]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> Post([FromBody]SetDTO setDTO)
        {
            var result = await _setService.AddSetAsync(setDTO);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> Delete(long Id)
        {
            var result = await _setService.DeleteSetAsync(Id);
            return Ok(new { Deleted = result });
        }
    }
}