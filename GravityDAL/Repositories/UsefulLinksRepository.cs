using AutoMapper;
using Domain.Entities;
using GravityDAL.Interfaces;

namespace GravityDAL.Repositories
{
    public class UsefulLinksRepository : Repository<UsefulLink>, IUsefulLinksRepository
    {
        public UsefulLinksRepository(GravityGymDbContext gravityGymDbContext, IMapper mapper) : base(gravityGymDbContext, mapper)
        {
            
        }
    }
}
