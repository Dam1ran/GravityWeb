using AutoMapper;
using Domain.Entities;
using GravityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravityWeb.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<PersonalInfo, PersonalInfoDTO>();
            CreateMap<PersonalInfoDTO, PersonalInfo>();//.ForMember(dest=>dest.,act=>act.Ignore());
        }
    }
}
