using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravityServices.Services
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

        public async Task<ApplicationUser> AddPersonalClientAsync(string coachEmail,string clientEmail)
        {            
            var coach = await _userManager.FindByEmailAsync(coachEmail);
            var client = await _userManager.FindByEmailAsync(clientEmail);

            if (coach!=null && client!=null)
            {                   
                coach.PersonalClients = new List<AppUserCoach>() { new AppUserCoach { CoachId = coach.Id, ApplicationUserId = client.Id }};
                await _userManager.UpdateAsync(coach);
                return coach;
            }

            throw new Exception($"Wrong Coach or Client Email");
        }

        public async Task RemoveAllPersonalClientsFromCoachAsync(string coachEmail)
        {
            var coach = await _userManager.FindByEmailAsync(coachEmail);
            await _appUserCoachRepository.RemoveAllPersonalClientsFromCoachAsync(coach.Id);            
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

        public async Task<AppUserCoach> RemovePersonalClientFromCoachAsync(string coachEmail, string clientEmail)
        {            
            var coach = await _userManager.FindByEmailAsync(coachEmail);
            var client = await _userManager.FindByEmailAsync(clientEmail);

            if (coach != null && client != null)
            {                
                return await _appUserCoachRepository.RemovePersonalClientFromCoachAsync(coach.Id, client.Id);
            }

            throw new Exception($"Wrong Coach or Client Email");

        }

        public async Task<IList<ClientDTO>> GetPersonalClientsAsync(string coachEmail)
        {
            var coach = await _userManager.FindByEmailAsync(coachEmail);
            
            return await _appUserCoachRepository
                .GetAllWithIncludeAsync(pc=>pc.ApplicationUser).Where(c=>c.CoachId==coach.Id)
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
