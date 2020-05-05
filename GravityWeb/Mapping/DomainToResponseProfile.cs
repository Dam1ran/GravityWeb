using AutoMapper;
using Domain.Entities;
using Domain.Entities.WorkoutEntities;
using GravityDAL.DTO;
using GravityDTO;
using GravityDTO.WORoutine;
using System.Linq;

namespace GravityWeb.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<PersonalInfo, PersonalInfoDTO>();
            CreateMap<PersonalInfoDTO, PersonalInfo>();//.ForMember(dest=>dest.,act=>act.Ignore());
            CreateMap<WoRoutine, WoRoutineDTO>();
            CreateMap<Workout, WorkoutDTO>();
            CreateMap<WorkoutDTO, Workout>();
            CreateMap<Exercise, ExerciseDTO>()
                .ForMember(x=>x.Name, y=>y.MapFrom(z=>z.ExerciseTemplate.Name));
            CreateMap<ExerciseSet, ExerciseSetDTO>();

            CreateMap<ExerciseTemplate, ExerciseTemplateDTO>()
                .ForMember(x=>x.PrimaryMuscle, y=>y.MapFrom(z=>z.PrimaryMuscle.Name))
                .ForMember(x => x.SecondaryMuscle, y => y.MapFrom(z => z.SecondaryMuscle.Name));

            CreateMap<ExerciseTemplateDTO,ExerciseTemplate>()
                .ForMember(x=>x.PrimaryMuscle, act=>act.Ignore())
                .ForMember(x=>x.SecondaryMuscle, act=>act.Ignore());

            CreateMap<ApplicationUser, ApplicationUserDTO>()
                .ForMember(x=>x.FullName, y=>y.MapFrom(z=>z.PersonalInfo.FirstName+" "+z.PersonalInfo.LastName))
                .ForMember(x => x.CoachFullName, y => y.MapFrom(z => z.Coach.Coach.PersonalInfo.FirstName+ " " + z.Coach.Coach.PersonalInfo.LastName))
                .ForMember(x => x.coachId, y => y.MapFrom(z => z.Coach.Coach.Id))
                .ForMember(x => x.userRoleId, y => y.MapFrom(z => z.Roles.Select(w=>w.RoleId).FirstOrDefault()));
        }
    }
}
