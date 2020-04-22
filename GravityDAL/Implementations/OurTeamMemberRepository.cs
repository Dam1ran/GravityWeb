using AutoMapper;
using Domain.Entities;
using GravityDAL.Interfaces;

namespace GravityDAL.Implementations
{
    public class OurTeamMemberRepository : Repository<OurTeamMember>, IOurTeamMemberRepository
    {
        public OurTeamMemberRepository(GravityGymDbContext gravityGymDbContext, IMapper mapper) : base(gravityGymDbContext, mapper)
        {

        }
    }
}
