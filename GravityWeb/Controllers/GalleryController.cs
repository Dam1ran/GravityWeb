using GravityServices.Interfaces;
using GravityWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        #region INITIALIZATIONS
        
        private readonly IGetFBImageUrlsService _getFBImageUrlsService;
        private readonly AppSettings _appsettings;

        public GalleryController(            
            IOptions<AppSettings> appSettings,
            IGetFBImageUrlsService getFBImageUrlsService
            )
        {
            _getFBImageUrlsService = getFBImageUrlsService;
            _appsettings = appSettings.Value;
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> GalleryPage([FromForm]string navigation)
        {
            var galImagesUrlDto = await _getFBImageUrlsService.GetImagesUrlAsync(navigation, _appsettings.FBAccessToken);
            return Ok(galImagesUrlDto);
        }

    }
}