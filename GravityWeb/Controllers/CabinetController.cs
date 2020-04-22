using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDAL.PageModels;
using GravityDTO;
using GravityDTO.WORoutine;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinetController : ControllerBase
    {

        public string UserEmail { get => User.FindFirst(ClaimTypes.NameIdentifier)?.Value; }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPersonalInfoService _personalInfoService;
        private readonly IUserService _userService;
        private readonly ICoachService _coachService;
        private readonly IPersonalInfoRepository _personalInfoRepository;
        private readonly IExerciseTemplateService _exerciseTemplateService;
        private readonly IMuscleRepository _muscleRepository;
        private readonly IWoRoutineService _woRoutineService;

        public CabinetController(
            UserManager<ApplicationUser> userManager,
            IPersonalInfoService personalInfoService,
            IUserService userService,
            ICoachService coachService,
            IPersonalInfoRepository personalInfoRepository,
            IMuscleRepository muscleRepository,
            IExerciseTemplateService exerciseTemplateService,
            IWoRoutineService woRoutineService)
        {
            _userManager = userManager;
            _personalInfoService = personalInfoService;
            _userService = userService;
            _coachService = coachService;
            _personalInfoRepository = personalInfoRepository;
            _exerciseTemplateService = exerciseTemplateService;
            _muscleRepository = muscleRepository;
            _woRoutineService = woRoutineService;
        }

        [HttpGet("getpersonalinfo")]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> GetPersonalInfo()
        {           
            var user = await _userManager.FindByEmailAsync(UserEmail);

            if (user!=null)
            {
                var personalInfo = await _personalInfoService.GetByUserId(user.Id);

                return Ok( personalInfo );
            }

            return BadRequest();
        }

        /// <summary>
        /// Saves Personal Info
        /// </summary>
        /// <param name="personalInfoDTO"></param>
        /// <returns></returns>
        [HttpPost("savepersonalinfo")]
        [Authorize(Policy = "RequireLoggedIn")]        
        public async Task<IActionResult> SavePersonalInfo([FromBody]PersonalInfoDTO personalInfoDTO)
        {

            var user = await _userManager.FindByEmailAsync(UserEmail);

            if (user!=null)
            {
                var response = await _personalInfoService.SavePersonalInfo(personalInfoDTO,user.Id);             
            
                return Ok(response);
               
            }

            return BadRequest();
        
        }

        [HttpPost("getusers")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetUsers([FromBody]GetUserRequestDTO getUserRequestDTO)
        {           
            var userDTOsResponse = await _personalInfoRepository
            .GetUsers(
                    getUserRequestDTO.filter,
                    getUserRequestDTO.page,
                    getUserRequestDTO.pageSize
                );

            return Ok(userDTOsResponse);
            
        }

        [HttpGet("getcoaches")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetCoaches()
        {
            
            var coachDTOs = await _coachService.GetCoachesAsync();
            
            return Ok(coachDTOs);

        }

        [HttpPost("saveuserrole")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> SaveUserRole([FromBody]SaveUserRoleDTO saveUserRoleDTO)
        {            

            var result = await _userService.UpdateRole(saveUserRoleDTO.userEmail, saveUserRoleDTO.roleId);

            if (result)
            {
                return Ok();
            }

            return BadRequest();

        }

        [HttpPost("assigncoach")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> AssignCoach([FromBody]ActionCoachDTO actionCoachDTO)
        {

            var user = await _coachService.AddPersonalClient(actionCoachDTO.coachEmail, actionCoachDTO.clientEmail);                       

            if (user!=null)
            {
                return Ok();
            }

            return BadRequest();

        }        

        [HttpPost("unassigncoach")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UnassignCoach([FromBody]ActionCoachDTO actionCoachDTO)
        {

            var result = await _coachService.RemovePersonalClientFromCoach(actionCoachDTO.coachEmail,actionCoachDTO.clientEmail);

            if (result)
            {
                return Ok();
            }

            return BadRequest();

        }

        [HttpGet("getmyclients")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> GetMyClients()
        {

            var clients = await _coachService.GetPersonalClients(UserEmail);

            return Ok(clients);            

        }

        [HttpPost("saveexercisetemplate")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> SaveExerciseTemplate([FromBody]ExerciseTemplateDTO exerciseTemplateDTO)
        {
            var result = await _exerciseTemplateService.SaveAsync(exerciseTemplateDTO);

            return Ok(new { Saved = result });
        }

        [HttpPost("getexercisetemplates")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> GetExerciseTemplates([FromBody]PaginatedRequest paginatedRequest)
        {
            var result = await _exerciseTemplateService.GetExerciseTemplates(paginatedRequest);

            return Ok(result);
        }

        [HttpGet("getmuscles")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> GetMuscles()
        {
            var muscles = await _muscleRepository.GetAllAsync();

            return Ok(muscles);

        }

        [HttpDelete("deleteexercisetemplate/{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> DeleteExerciseTemplate(long Id)
        {
            var result = await _exerciseTemplateService.DeleteAsync(Id);

            return Ok(new { Deleted = result });

        }

        [HttpPost("addworkoutroutine")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> AddWorkoutRoutine([FromBody]WoRoutineDTO woRoutineDTO)
        {
            var result = await _woRoutineService.AddRoutine(woRoutineDTO);

            return Ok(result);

        }
        [HttpGet("getworkoutroutinesname")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> GetWorkoutRoutinesName()
        {
            var result = await _woRoutineService.GetRoutines();

            return Ok(result);

        }

        [HttpDelete("deleteworkoutroutine/{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> DeleteWorkoutRoutine(long Id)
        {
            var result = await _woRoutineService.DeleteRoutine(Id);

            return Ok(new { Deleted = result });

        }

        [HttpGet("getroutine/{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> GetRoutine(long Id)
        {
            var result = await _woRoutineService.GetRoutine(Id);

            return Ok(result);

        }

        [HttpPost("createworkoutinroutine")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> CreateWorkoutInRoutine([FromBody]WorkoutDTO workoutDTO)
        {
            var result = await _woRoutineService.CreateWorkout(workoutDTO);

            if (result!=null)
            {
                return Ok(result);
            }

            return BadRequest("Could Not Add Workout");
        }

        [HttpPut("updateworkoutdescription")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> UpdateWorkoutDescription([FromBody]WorkoutDTO workoutDTO)
        {
            var result = await _woRoutineService.UpdateWorkoutDescription(workoutDTO);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Could Not Update Workout Description");
        }

        [HttpGet("getworkout/{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> GetWorkout(long Id)
        {
            var result = await _woRoutineService.GetWorkout(Id);

            return Ok(result);

        }

        [HttpDelete("deleteworkout/{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> DeleteWorkout(long Id)
        {
            var result = await _woRoutineService.DeleteWorkout(Id);

            return Ok(new { Deleted = result });

        }

        [HttpDelete("deletelastworkoutfromroutine/{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> DeleteLastWorkoutFromRoutine(long Id)
        {
            var result = await _woRoutineService.DeleteLastWorkoutFromRoutine(Id);
            if (result!=null)
            {
                return Ok(result);
            }

            return BadRequest();

        }

    }
}