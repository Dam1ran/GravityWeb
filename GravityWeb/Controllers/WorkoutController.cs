using GravityDTO.WorkoutModels;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpPost]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> Post([FromBody]WorkoutDTO workoutDTO)
        {
            var result = await _workoutService.CreateWorkoutAsync(workoutDTO);
            return Ok(result);
        }

        [HttpPut("updateworkoutdescription")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> UpdateWorkoutDescription([FromBody]WorkoutDTO workoutDTO)
        {
            var result = await _workoutService.UpdateWorkoutDescriptionAsync(workoutDTO);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> GetWorkout(long Id)
        {
            var workout = await _workoutService.GetWorkoutAsync(Id);
            return Ok(workout);
        }


        [HttpDelete("deletelastworkoutfromroutine/{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> DeleteLastWorkoutFromRoutine(long Id)
        {
            var workout = await _workoutService.DeleteLastWorkoutFromRoutineAsync(Id);
            return Ok(workout);
        }
    }
}