using Domain.Entities;
using GravityDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IOurTeamMemberService
    {
        Task<OurTeamMember> SaveAsync(string WRpath, OurTeamMemberData ourTeamMemberData);

    }
}
