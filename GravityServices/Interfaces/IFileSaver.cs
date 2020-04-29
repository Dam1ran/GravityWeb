using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IFileSaver
    {
        Task<string> Save(string envString, string uploadFolderName, IFormFile file);
    }
}
