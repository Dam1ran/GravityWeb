using Domain.Entities;
using GravityDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IUserService
    {        
        Task<bool> UpdateRole(string userEmail, long roleId);

    }
}
