using GravityDTO;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IGetFBImageUrlsService
    {
        public Task<GalleryImagesDTO> GetImagesUrl(string nav,string token);
    }
}
