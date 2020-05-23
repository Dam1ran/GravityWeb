using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IFileSaver
    {
        Task<string> SaveAsync(string envString, string uploadFolderName, IFormFile file);
    }
}
