using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravityServices.Implementations
{
    public class CoachService : ICoachService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPersonalInfoRepository _personalInfoRepository;
        private readonly IPersonalClientRepository _personalClientRepository;

        public CoachService(
            UserManager<ApplicationUser> userManager,
            IPersonalInfoRepository personalInfoRepository,
            IPersonalClientRepository personalClientRepository
            )
        {
            _userManager = userManager;
            _personalInfoRepository = personalInfoRepository;
            _personalClientRepository = personalClientRepository;
        }

        public async Task<ApplicationUser> AddPersonalClient(string coachEmail,string clientEmail)
        {
            if (string.IsNullOrEmpty(coachEmail) && string.IsNullOrEmpty(clientEmail))
            {
                return null;
            }

            var coach = await _userManager.FindByEmailAsync(coachEmail);
            var client = await _userManager.FindByEmailAsync(clientEmail);


            if (coach!=null && client!=null)
            {                   
                coach.Clients = new List<PersonalClient>() { new PersonalClient { Email = clientEmail } };
                await _userManager.UpdateAsync(coach);
            }            

            return coach;

        }

        public async Task<ApplicationUser> RemovePersonalClientsFromCoach(string coachEmail)
        {
            var user = await _userManager.FindByEmailAsync(coachEmail);

            if (user != null)
            {
                var coachClients = await _personalClientRepository.GetPersonalClients(user.Id).ToListAsync();
                                
                await _personalClientRepository.RemovePersonalClients(coachClients);
                
            }

            return user;

        }

        public async Task<IList<CoachDTO>> GetCoachesAsync()
        {
            var users = _userManager.Users;            

            var coachesDTOs = await users
                .Join(_personalInfoRepository.GetUserRoles(),
                p => p.Id,
                c => c.UserId,
                (p, c) =>
                new
                {
                    p.Id,
                    p.PersonalInfo.FirstName,
                    p.PersonalInfo.LastName,
                    p.Email,
                    c.RoleId
                })
                .Where(r=>r.RoleId==2L)
                .Select(o=> new CoachDTO 
                {
                    Id = o.Id,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    Email = o.Email
                }).ToListAsync();

            return coachesDTOs;

        }

        public async Task<ApplicationUser> RemovePersonalClientsFromCoach(string coachEmail, string clientEmail)
        {
            if (string.IsNullOrEmpty(coachEmail) && string.IsNullOrEmpty(clientEmail))
            {
                return null;
            }

            var user = await _userManager.FindByEmailAsync(coachEmail);

            if (user != null)
            {
                var coachClient = _personalClientRepository
                    .GetPersonalClients(user.Id)
                    .Where(c=>c.Email == clientEmail)
                    .FirstOrDefault();

                await _personalClientRepository.RemovePersonalClient(coachClient);

            }

            return user;
        }
    }
}
