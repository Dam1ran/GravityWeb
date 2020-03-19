using Domain.Entities;
using GravityDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GravityDAL.Implementations
{
    public class UsefulLinksRepository : Repository<UsefulLink>, IUsefulLinksRepository
    {
        public UsefulLinksRepository(GravityGymDbContext gravityGymDbContext) : base(gravityGymDbContext)
        {
            
        }
    }
}
