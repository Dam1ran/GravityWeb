using GravityDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IFileSaver
    {
        Task<string> Save(string envString, IFormFile file);
    }
}
