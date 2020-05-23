using Domain.Entities;
using GravityDAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly IUsefulLinksRepository _usefulLinksRepository;
        public LinkController(
            IUsefulLinksRepository usefulLinksRepository)
        {
            _usefulLinksRepository = usefulLinksRepository;
        }


        [HttpGet]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _usefulLinksRepository.GetAllAsync());
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> Delete(int Id)
        {            
            await _usefulLinksRepository.DeleteAsync(Id);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "RequireCoachRole")]
        public async Task<IActionResult> Post([FromBody]UsefulLink usefulLink)
        {
            var link = await _usefulLinksRepository.AddAsync(usefulLink);
            return Ok(link);
        }

    }
}