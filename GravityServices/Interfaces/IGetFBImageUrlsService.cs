using GravityDTO;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IGetFBImageUrlsService
    {
        Task<GalleryImagesDTO> GetImagesUrlAsync(string nav,string token);
    }
}
