using AutoMapper;
using Domain.Entities;
using Domain.Entities.WorkoutEntities;
using GravityDAL.Models;
using GravityDTO;
using GravityDTO.WorkoutModels;
using System.Linq;

namespace GravityWeb.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<PersonalInfo, PersonalInfoDTO>();
            CreateMap<PersonalInfoDTO, PersonalInfo>();//.ForMember(dest=>dest.,act=>act.Ignore());
            CreateMap<Routine, RoutineDTO>();
            CreateMap<Workout, WorkoutDTO>();
            CreateMap<WorkoutDTO, Workout>();
            CreateMap<Exercise, ExerciseDTO>()
                .ForMember(x=>x.Name, y=>y.MapFrom(z=>z.ExerciseTemplate.Name));
            CreateMap<Set, SetDTO>();
            CreateMap<SetDTO, Set>();

            CreateMap<ExerciseTemplate, ExerciseTemplateDTO>()
                .ForMember(x=>x.PrimaryMuscle, y=>y.MapFrom(z=>z.PrimaryMuscle.Name))
                .ForMember(x => x.SecondaryMuscle, y => y.MapFrom(z => z.SecondaryMuscle.Name));

            CreateMap<ExerciseTemplateDTO,ExerciseTemplate>()
                .ForMember(x=>x.PrimaryMuscle, act=>act.Ignore())
                .ForMember(x=>x.SecondaryMuscle, act=>act.Ignore());

            CreateMap<ApplicationUser, ApplicationUserModel>()
                .ForMember(x=>x.FullName, y=>y.MapFrom(z=>z.PersonalInfo.FirstName+" "+z.PersonalInfo.LastName))
                .ForMember(x => x.CoachFullName, y => y.MapFrom(z => z.Coach.Coach.PersonalInfo.FirstName+ " " + z.Coach.Coach.PersonalInfo.LastName))
                .ForMember(x => x.coachId, y => y.MapFrom(z => z.Coach.Coach.Id))
                .ForMember(x => x.userRoleId, y => y.MapFrom(z => z.Roles.Select(w=>w.RoleId).FirstOrDefault()));
        }
    }
}
