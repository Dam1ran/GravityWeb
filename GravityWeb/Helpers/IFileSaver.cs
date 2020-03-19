using System.Threading.Tasks;

namespace GravityWeb.Helpers
{
    public interface IFileSaver
    {        
        Task<string> Save(string envString, FileUploadAPI objFile);
    }
}