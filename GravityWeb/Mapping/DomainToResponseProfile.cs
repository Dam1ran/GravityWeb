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
        }
    }
}
