using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IUsefulLinkService
    {
        Task<UsefulLink> SaveAsync(UsefulLink usefulLink);
    }
}
