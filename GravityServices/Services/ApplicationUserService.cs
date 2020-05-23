using AutoMapper;
using Domain.Entities;
using GravityDAL.Models;
using GravityDAL.Interfaces;
using GravityDAL.PageModels;
using GravityServices.Utils;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;



namespace GravityServices.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IMapper _mapper;        
        private readonly IQueryable<ApplicationUser> _users;
        private readonly IPersonalInfoRepository _personalInfoRepository;

        public ApplicationUserService(
            UserManager<ApplicationUser> userManager,
            IPersonalInfoRepository personalInfoRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _users = userManager.Users;
            _personalInfoRepository = personalInfoRepository;
        }
        public async Task<PaginatedResult<ApplicationUserModel>> GetUsersAsync(PaginatedRequest paginatedRequest)
        {
            var users = await _users.CreatePaginatedResultAsync<ApplicationUserModel>(paginatedRequest, _mapper);

            foreach (var appUserDTO in users.Items)
            {
                appUserDTO.RoleString = _personalInfoRepository.GetRoleName(appUserDTO.userRoleId);
            }            

            return users;
        }
    }
}
