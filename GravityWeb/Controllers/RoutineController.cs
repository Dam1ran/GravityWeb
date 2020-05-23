using GravityDTO.WorkoutModels;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutineController : ControllerBase
    {
        private readonly IRoutineService _routineService;
        public RoutineController(IRoutineService routineService)
        {
            _routineService = routineService;
        }

        [HttpGet("{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> Get(long Id)
        {
            var routine = await _routineService.GetRoutineAsync(Id);
            return Ok(routine);
        }

        [HttpGet("getnames")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> GetNames()
        {
            var namesList = await _routineService.GetRoutinesNameAsync();
            return Ok(namesList);
        }

        [HttpPost]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> Post([FromBody]RoutineDTO routineDTO)
        {
            var routine = await _routineService.AddRoutineAsync(routineDTO);
            return Ok(routine);
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> DeleteWorkoutRoutine(long Id)
        {
            var result = await _routineService.DeleteRoutineAsync(Id);
            return Ok(new { Deleted = result });
        }

    }
}