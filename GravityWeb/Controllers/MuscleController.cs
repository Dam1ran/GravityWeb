using GravityDAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuscleController : ControllerBase
    {
        private readonly IMuscleRepository _muscleRepository;
        public MuscleController(IMuscleRepository muscleRepository)
        {
            _muscleRepository = muscleRepository;
        }

        [HttpGet]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> GetMuscles()
        {
            var muscles = await _muscleRepository.GetAllAsync();
            return Ok(muscles);
        }
    }
}