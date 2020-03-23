using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<OurTeamMember> SaveAsync(string WRpath, OurTeamMemberData ourTeamMemberData)
        {
            var responseAvatarFile = await _fileSaver.Save(WRpath, ourTeamMemberData.avatarFile);
            var responseImageFile = await _fileSaver.Save(WRpath, ourTeamMemberData.imageFile);

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

                var dbTeamMember = await _ourTeamMemberRepository.AddAsync(teamMember);

                if (dbTeamMember != null)
                {
                    return dbTeamMember;
                }
            }

            return null;
        }
    }
}
