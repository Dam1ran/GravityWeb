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
        private readonly IAppUserCoachRepository _appUserCoachRepository;

        public CoachService(
            UserManager<ApplicationUser> userManager,
            IAppUserCoachRepository appUserCoachRepository)
        {
            _userManager = userManager;
            _appUserCoachRepository = appUserCoachRepository;
        }

        public async Task<ApplicationUser> AddPersonalClient(string coachEmail,string clientEmail)
        {
            if (string.IsNullOrEmpty(coachEmail) || string.IsNullOrEmpty(clientEmail))
            {
                return null;
            }

            var coach = await _userManager.FindByEmailAsync(coachEmail);
            var client = await _userManager.FindByEmailAsync(clientEmail);


            if (coach!=null && client!=null)
            {                   
                coach.PersonalClients = new List<AppUserCoach>() { new AppUserCoach { CoachId = coach.Id, ApplicationUserId = client.Id }};
                await _userManager.UpdateAsync(coach);
            }            

            return coach;

        }

        public async Task<bool> RemoveAllPersonalClientsFromCoach(string coachEmail)
        {
            var coach = await _userManager.FindByEmailAsync(coachEmail);

            if (coach != null)
            {
                return await _appUserCoachRepository.RemoveAllPersonalClientsFromCoach(coach.Id);
            }

            return false;

        }

        public async Task<IList<CoachDTO>> GetCoachesAsync()
        {
            return await _userManager.Users
                .Where(u=>u.Roles.FirstOrDefault().RoleId==2L)
                .Select(x=> new CoachDTO
                { 
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.PersonalInfo.FirstName,
                    LastName = x.PersonalInfo.LastName
                }).ToListAsync();

        }

        public async Task<bool> RemovePersonalClientFromCoach(string coachEmail, string clientEmail)
        {
            if (string.IsNullOrEmpty(coachEmail) && string.IsNullOrEmpty(clientEmail))
            {
                return false;
            }

            var coach = await _userManager.FindByEmailAsync(coachEmail);
            var client = await _userManager.FindByEmailAsync(clientEmail);

            if (coach != null && client != null)
            {                
                var result = await _appUserCoachRepository.RemovePersonalClientFromCoach( coach.Id, client.Id );
                return result;
            }

            return false;
        }

        public async Task<IList<ClientDTO>> GetPersonalClients(string coachEmail)
        {
            var coach = await _userManager.FindByEmailAsync(coachEmail);
            
            return await _appUserCoachRepository
                .GetAllWithInclude(pc=>pc.ApplicationUser).Where(c=>c.CoachId==coach.Id)
                .Select(x=> new ClientDTO
                {   
                    Id = x.ApplicationUser.Id,
                    Email = x.ApplicationUser.Email,
                    FirstName = x.ApplicationUser.PersonalInfo.FirstName,
                    LastName = x.ApplicationUser.PersonalInfo.LastName
                }).ToListAsync();

        }
    }
}
