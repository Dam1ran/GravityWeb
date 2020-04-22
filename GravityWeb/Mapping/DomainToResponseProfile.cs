using AutoMapper;
using Domain.Entities;
using Domain.Entities.WorkoutEntities;
using GravityDTO;
using GravityDTO.WORoutine;

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
            //CreateMap<ExerciseTemplate, ExerciseTemplateDTO>();
            CreateMap<ExerciseTemplate, ExerciseTemplateDTO>()
                .ForMember(x=>x.PrimaryMuscle, y=>y.MapFrom(z=>z.PrimaryMuscle.Name))
                .ForMember(x => x.SecondaryMuscle, y => y.MapFrom(z => z.SecondaryMuscle.Name));

            CreateMap<ExerciseTemplateDTO,ExerciseTemplate>()
                .ForMember(x=>x.PrimaryMuscle, act=>act.Ignore())
                .ForMember(x=>x.SecondaryMuscle, act=>act.Ignore());
        }
    }
}
