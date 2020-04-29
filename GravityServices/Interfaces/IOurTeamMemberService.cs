using Domain.Entities;
using GravityDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IOurTeamMemberService
    {
        Task<OurTeamMember> SaveAsync(string WRpath, string uploadFolderName, OurTeamMemberData ourTeamMemberData);
        Task<IList<OurTeamMember>> GetAllAsync(string baseUrl, string uploadFolderName);

    }
}
