using Domain.Entities;
using GravityDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface ITeamMemberService
    {
        Task<TeamMember> SaveAsync(string WRpath, string uploadFolderName, TeamMemberData ourTeamMemberData);
        Task<IList<TeamMember>> GetAllAsync(string baseUrl, string uploadFolderName);
    }
}
