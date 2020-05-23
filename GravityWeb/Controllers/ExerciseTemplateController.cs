using GravityDAL.PageModels;
using GravityDTO;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseTemplateController : ControllerBase
    {
        private readonly IExerciseTemplateService _exerciseTemplateService;
        public ExerciseTemplateController(IExerciseTemplateService exerciseTemplateService)
        {
            _exerciseTemplateService = exerciseTemplateService;
        }


        [HttpPost("getexercisetemplates")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> GetExerciseTemplates([FromBody]PaginatedRequest paginatedRequest)
        {
            var exerciseTemplatePage = await _exerciseTemplateService.GetExerciseTemplatesAsync(paginatedRequest);
            return Ok(exerciseTemplatePage);
        }

        [HttpGet("{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> Get(long Id)
        {
            var exerciseTemplate = await _exerciseTemplateService.GetExerciseTemplateAsync(Id);
            return Ok(exerciseTemplate);
        }

        [HttpPost]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> Post([FromBody]ExerciseTemplateDTO exerciseTemplateDTO)
        {
            var result = await _exerciseTemplateService.SaveAsync(exerciseTemplateDTO);
            return Ok(new { Saved = result });
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> DeleteExerciseTemplate(long Id)
        {
            var result = await _exerciseTemplateService.DeleteAsync(Id);
            return Ok(new { Deleted = result });
        }
    }
}