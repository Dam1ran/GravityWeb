using AutoMapper;
using Domain.Auth;
using Domain.Entities;
using GravityDAL;
using GravityDAL.Implementations;
using GravityDAL.Interfaces;
using GravityServices.Implementations;
using GravityServices.Interfaces;
using GravityWeb.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace GravityWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<GravityGymDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(Configuration.GetConnectionString("GravityGymConnectionString"));
            });


            services.AddIdentity<ApplicationUser, Role>(options=>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<GravityGymDbContext>().AddDefaultTokenProviders();


            services.AddCors(options=>options.AddPolicy("Cors",builder=>
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));


            services.AddControllers();

            //helpers
            services.AddScoped<IFileSaver,FileSaver>();
            services.AddScoped<IGetFBImageUrlsService, GetFBImageUrlsService>();


            //repos and services
            services.AddScoped<IGymSessionScheduleRepository, GymSessionScheduleRepository>();
            services.AddScoped<IUsefulLinksRepository, UsefulLinksRepository>();
            services.AddScoped<IUsefulLinkService, UsefulLinkService>();
            services.AddScoped<IDayScheduleService, DayScheduleService>();
            services.AddScoped<IOurTeamMemberRepository, OurTeamMemberRepository>();
            services.AddScoped<IOurTeamMemberService, OurTeamMemberService>();
            services.AddScoped<IPersonalInfoRepository, PersonalInfoRepository>();
            services.AddScoped<IPersonalClientRepository, PersonalClientRepository>();
            services.AddScoped<IPersonalInfoService, PersonalInfoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICoachService, CoachService>();


            //services.AddScoped<IRepository<GymSessionSchedule>, Repository<GymSessionSchedule>>();


            //configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            //Auth middleware
            services.AddAuthentication(opt => {
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = appSettings.Site,
                    ValidAudience = appSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            services.AddAuthorization(options=>
            {
                options.AddPolicy("RequireLoggedIn",  policy => policy.RequireRole("Client","Coach","Manager","Admin").RequireAuthenticatedUser());
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin").RequireAuthenticatedUser());
            });

            services.AddAutoMapper(typeof(Startup));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("Cors");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run(async (context) => { await context.Response.WriteAsync("Can't find anything");});
        }
    }
}
