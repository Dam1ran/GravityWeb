using AutoMapper;
using Domain.Auth;
using Domain.Entities;
using GravityDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GravityDAL.Repositories
{
    public class PersonalInfoRepository : Repository<PersonalInfo>, IPersonalInfoRepository
    {
        private readonly DbSet<Role> _roles;

        public PersonalInfoRepository(GravityGymDbContext gravityGymDbContext, IMapper mapper) : base(gravityGymDbContext, mapper)
        {
            _roles = gravityGymDbContext.Roles;
        }

        public async Task<PersonalInfo> GetPersonalInfoByUserIdAsync(long Id)
        {
            return await _dbSet.Where(pi => pi.ApplicationUserId == Id).FirstOrDefaultAsync();            
        }
      

        public string GetRoleName(long roleId)
        {
            return _roles.Where(r=>r.Id==roleId).Select(rs=>rs.Name).First();
        }

    }
}
