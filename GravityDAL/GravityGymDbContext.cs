using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GravityDAL
{
    public class GravityGymDbContext : IdentityDbContext
    {
        public GravityGymDbContext(DbContextOptions<GravityGymDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData
                (
                    new { Id = "1", Name="Client", NormalizedName="CLIENT" },
                    new { Id = "2", Name="Coach", NormalizedName="COACH" },
                    new { Id = "3", Name="Admin",  NormalizedName="ADMIN"  }
                );
        }

        public DbSet<GymSessionSchedule> GymSessionsSchedule { get; set; }
        public DbSet<UsefulLink> UsefulLinks { get; set; }
        public DbSet<OurTeamMember> OurTeamMembers { get; set; }
    }
}
