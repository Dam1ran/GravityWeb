using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Interfaces;
using GravityServices.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GravityServices.Services
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly IFileSaver _fileSaver;
        private readonly ITeamMemberRepository _ourTeamMemberRepository;

        public TeamMemberService(IFileSaver fileSaver, ITeamMemberRepository ourTeamMemberRepository)
        {
            _fileSaver = fileSaver;
            _ourTeamMemberRepository = ourTeamMemberRepository;
        }

        public async Task<IList<TeamMember>> GetAllAsync(string baseUrl, string uploadFolderName)
        {
            var result = await _ourTeamMemberRepository.GetAllAsync();

            foreach (var item in result)
            {
                item.AvatarUrl = item.AvatarUrl.MakeUrl(baseUrl, uploadFolderName);
                item.ImageUrl = item.ImageUrl.MakeUrl(baseUrl, uploadFolderName);
            }

            return result;
        }

        public async Task<TeamMember> SaveAsync(string WRpath, string uploadFolderName, TeamMemberData ourTeamMemberData)
        {
            var responseAvatarFile = await _fileSaver.SaveAsync(WRpath, uploadFolderName, ourTeamMemberData.avatarFile);
            var responseImageFile = await _fileSaver.SaveAsync(WRpath, uploadFolderName, ourTeamMemberData.imageFile);

            if (responseAvatarFile != "Failed" || responseImageFile != "Failed")
            {
                var teamMember = new TeamMember
                {
                    AvatarUrl = responseAvatarFile,
                    FullName = ourTeamMemberData.fullName,
                    Activity = ourTeamMemberData.activity,
                    ImageUrl = responseImageFile,
                    Description = ourTeamMemberData.description,
                    Moto = ourTeamMemberData.moto
                };
                return await _ourTeamMemberRepository.AddAsync(teamMember);                
            }

            return null;
        }
    }
}
