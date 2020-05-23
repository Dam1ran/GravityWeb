using GravityDTO.WorkoutModels;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;
        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpPost]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> Post([FromBody]ExerciseDTO exerciseDTO)
        {
            var result = await _exerciseService.AddExerciseToWorkoutAsync(exerciseDTO);
            return Ok(result);
        }

        [HttpGet("getexercisesfromworkout/{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> GetExercisesFromWorkout(long Id)
        {
            var result = await _exerciseService.GetExercisesFromWorkoutAsync(Id);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> DeleteExercise(long Id)
        {
            var result = await _exerciseService.DeleteExerciseAsync(Id);
            return Ok(new { Deleted = result });

        }

        [HttpPost("swapup")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> SwapUp([FromBody]ExerciseDTO exerciseDTO)
        {
            var result = await _exerciseService.SwapAsync(exerciseDTO, true);
            return Ok(result);
        }

        [HttpPost("swapdown")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> SwapDown([FromBody]ExerciseDTO exerciseDTO)
        {
            var result = await _exerciseService.SwapAsync(exerciseDTO, false);
            return Ok(result);
        }
        
    }
}