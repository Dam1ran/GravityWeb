using Domain.Auth;
using Domain.Entities;
using Domain.Entities.WorkoutEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GravityDAL
{
    public class GravityGymDbContext : IdentityDbContext<ApplicationUser, Role,long,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
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
        public DbSet<AppUserCoach> AppUserCoaches { get; set; }
        public DbSet<ExerciseTemplate> ExerciseTemplates { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<WoRoutine> WoRoutines  { get; set; }
        

        private void ApplyIdentityMapConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "Auth");
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

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Roles)
                .WithOne()
                .HasForeignKey(x => x.UserId);


            modelBuilder.Entity<AppUserCoach>()
                .HasIndex(e => new { e.CoachId, e.ApplicationUserId }).IsUnique();

            modelBuilder.Entity<AppUserCoach>()
                .HasOne(x => x.ApplicationUser)
                .WithOne(x => x.Coach)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppUserCoach>()
                .HasOne(x => x.Coach)
                .WithMany(x => x.PersonalClients)
                .HasForeignKey(x => x.CoachId);

            modelBuilder.Entity<Muscle>().HasData
                (
                    new Muscle { Id=1L, Name="Calves"},
                    new Muscle { Id=2L, Name="Quads"},
                    new Muscle { Id=3L, Name="Hamstrings"},
                    new Muscle { Id=4L, Name="Glutes"},
                    new Muscle { Id=5L, Name="Abs"},
                    new Muscle { Id=6L, Name="Core"},
                    new Muscle { Id=7L, Name="Lower Back"},
                    new Muscle { Id=8L, Name="Lats"},
                    new Muscle { Id=9L, Name="Traps"},
                    new Muscle { Id=10L, Name="Chest"},
                    new Muscle { Id=11L, Name="Neck"},
                    new Muscle { Id=12L, Name="Shoulders"},
                    new Muscle { Id=13L, Name="Triceps"},
                    new Muscle { Id=14L, Name="Biceps"},
                    new Muscle { Id=15L, Name="Forearms"}

                );
            

            modelBuilder.Entity<ExerciseTemplate>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<ExerciseTemplate>()
                .HasMany(x => x.Exercises)
                .WithOne(x => x.ExerciseTemplate)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Exercise>()
                .HasOne(x => x.Workout)
                .WithMany(x => x.Exercises)
                .OnDelete(DeleteBehavior.Cascade);          

        }

    }
}
