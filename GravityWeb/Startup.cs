using AutoMapper;
using Domain.Auth;
using Domain.Entities;
using GravityDAL;
using GravityDAL.Repositories;
using GravityDAL.Interfaces;
using GravityServices.Services;
using GravityServices.Interfaces;
using GravityWeb.Models;
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
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;
using GravityWeb.Middlewares;

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
            AddDbContext(services);

            ConfigureIdentity(services);                        

            services.AddControllers();            

            ConfigureMyServices(services);

            ConfigureRepositories(services);

            ConfigureAuthentication(services);

            ConfigureAuthorization(services);

            services.AddAutoMapper(typeof(Startup));

            AddSwagger(services);

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
                app.UseMiddleware<ErrorHandlingMiddleware>();
                //app.UseHsts();
            }

            var swaggerOptions = new SwaggerOptionsModel();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(opt =>
            {
                opt.RouteTemplate = swaggerOptions.JsonRoute;
            });

            app.UseSwaggerUI(opt=> 
            {
                opt.SwaggerEndpoint(swaggerOptions.UIEndpoint,swaggerOptions.Description);
            });

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
               
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(g =>
            {
                g.SwaggerDoc("v1", new OpenApiInfo { Title = "Gravity Site API", Version = "v1" });
            });
        }
        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsefulLinksRepository, UsefulLinksRepository>();
            services.AddScoped<IGymSessionScheduleRepository, GymSessionScheduleRepository>();
            services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
            services.AddScoped<IPersonalInfoRepository, PersonalInfoRepository>();
            services.AddScoped<IAppUserCoachRepository, AppUserCoachRepository>();

            services.AddScoped<IExerciseTemplateRepository, ExerciseTemplateRepository>();
            services.AddScoped<IMuscleRepository, MuscleRepository>();

            services.AddScoped<IRoutineRepository, RoutineRepository>();
            services.AddScoped<IWorkoutRepository, WorkoutRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<ISetRepository, SetRepository>();
        }
        private void ConfigureMyServices(IServiceCollection services)
        {
            services.AddScoped<IGymSessionScheduleService, GymSessionScheduleService>();
            services.AddScoped<ITeamMemberService, TeamMemberService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IPersonalInfoService, PersonalInfoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<IExerciseTemplateService, ExerciseTemplateService>();
            services.AddScoped<IRoutineService, RoutineService>();
            services.AddScoped<IWorkoutService, WorkoutService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<ISetService, SetService>();
            //helpers
            services.AddScoped<IFileSaver, FileSaver>();
            services.AddScoped<IGetFBImageUrlsService, GetFBImageUrlsService>();
        }
        private void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireLoggedIn", policy => policy.RequireRole("Client", "Coach", "Manager", "Admin").RequireAuthenticatedUser());
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin").RequireAuthenticatedUser());
                options.AddPolicy("RequireCoachRole", policy => policy.RequireRole("Coach").RequireAuthenticatedUser());
            });
        }
        private void ConfigureAuthentication(IServiceCollection services)
        {
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
        }
        private void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<GravityGymDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(Configuration.GetConnectionString("GravityGymConnectionString"));
            });
        }
        private void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, Role>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<GravityGymDbContext>().AddDefaultTokenProviders();
        }
    }
}
