using AutoMapper;
using Domain.Entities;
using GravityDAL.DTO;
using GravityDAL.Interfaces;
using GravityDAL.PageModels;
using GravityServices.Helpers;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;



namespace GravityServices.Implementations
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
        public async Task<PaginatedResult<ApplicationUserDTO>> GetUsers(PaginatedRequest paginatedRequest)
        {
            var users = await _users.CreatePaginatedResultAsync<ApplicationUserDTO>(paginatedRequest, _mapper);

            foreach (var appUserDTO in users.Items)
            {
                appUserDTO.RoleString = _personalInfoRepository.GetUserRole(appUserDTO.userRoleId);
            }            

            return users;
        }
    }
}
