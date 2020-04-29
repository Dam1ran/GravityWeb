using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Helpers;
using GravityServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GravityServices.Implementations
{
    public class OurTeamMemberService : IOurTeamMemberService
    {
        private readonly IFileSaver _fileSaver;
        private readonly IOurTeamMemberRepository _ourTeamMemberRepository;

        public OurTeamMemberService(IFileSaver fileSaver, IOurTeamMemberRepository ourTeamMemberRepository)
        {
            _fileSaver = fileSaver;
            _ourTeamMemberRepository = ourTeamMemberRepository;
        }

        public async Task<IList<OurTeamMember>> GetAllAsync(string baseUrl, string uploadFolderName)
        {
            var result = await _ourTeamMemberRepository.GetAllAsync();

            foreach (var item in result)
            {
                item.AvatarUrl = item.AvatarUrl.MakeUrl(baseUrl, uploadFolderName);
                item.ImageUrl = item.ImageUrl.MakeUrl(baseUrl, uploadFolderName);
            }

            return result;
        }

        public async Task<OurTeamMember> SaveAsync(string WRpath, string uploadFolderName, OurTeamMemberData ourTeamMemberData)
        {
            var responseAvatarFile = await _fileSaver.Save(WRpath, uploadFolderName, ourTeamMemberData.avatarFile);
            var responseImageFile = await _fileSaver.Save(WRpath, uploadFolderName, ourTeamMemberData.imageFile);

            if (responseAvatarFile != "Failed" || responseImageFile != "Failed")
            {
                var teamMember = new OurTeamMember
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
