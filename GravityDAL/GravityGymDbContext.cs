using Domain.Auth;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GravityDAL
{
    public class GravityGymDbContext : IdentityDbContext<User,Role,long,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    {
        public GravityGymDbContext(DbContextOptions<GravityGymDbContext> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            //optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>().HasData
                (
                    new { Id = 1L, Name="Client", NormalizedName="CLIENT" },
                    new { Id = 2L, Name="Coach", NormalizedName="COACH" },
                    new { Id = 3L, Name="Manager", NormalizedName="MANAGER" },
                    new { Id = 4L, Name="Admin",  NormalizedName="ADMIN"  }
                );

            ApplyIdentityMapConfiguration(builder);

        }

        public DbSet<GymSessionSchedule> GymSessionsSchedule { get; set; }
        public DbSet<UsefulLink> UsefulLinks { get; set; }
        public DbSet<OurTeamMember> OurTeamMembers { get; set; }
        public DbSet<PersonalInfo>  PersonalInfos { get; set; }
        public DbSet<PersonalClient> PersonalClients { get; set; }
        
        
        private void ApplyIdentityMapConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users", "Auth");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims", "Auth");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins", "Auth");
            modelBuilder.Entity<UserToken>().ToTable("UserRoles", "Auth");
            modelBuilder.Entity<Role>().ToTable("Roles", "Auth");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", "Auth");
            modelBuilder.Entity<UserRole>().ToTable("UserRole", "Auth");

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(x => x.PersonalInfo)
                .WithOne(x => x.ApplicationUser)
                .HasForeignKey<PersonalInfo>(x => x.ApplicationUserId);

            modelBuilder.Entity<PersonalClient>().HasIndex(p => new { p.Email, p.ApplicationUserId }).IsUnique();

        }
        
    }
}
