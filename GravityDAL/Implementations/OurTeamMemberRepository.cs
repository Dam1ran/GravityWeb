using Domain.Entities;
using GravityDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GravityDAL.Implementations
{
    public class OurTeamMemberRepository : Repository<OurTeamMember>, IOurTeamMemberRepository
    {
        public OurTeamMemberRepository(GravityGymDbContext gravityGymDbContext) : base(gravityGymDbContext)
        {

        }
    }
}
