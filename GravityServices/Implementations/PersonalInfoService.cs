using AutoMapper;
using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GravityServices.Implementations
{
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly IPersonalInfoRepository _personalInfoRepository;
        private readonly IMapper _mapper;

        public PersonalInfoService(
            IPersonalInfoRepository personalInfoRepository,
            IMapper mapper
            )
        {
            _personalInfoRepository = personalInfoRepository;
            _mapper = mapper;
        }

        public async Task<PersonalInfoDTO> GetByUserId(long Id)
        {
            var personalInfo = await _personalInfoRepository.GetPersonalInfoByUserId(Id);

            var personalInfoDTO = new PersonalInfoDTO();

            if (personalInfo == null)
            {               
                return personalInfoDTO;
            }
            else
            {

                personalInfoDTO = _mapper.Map<PersonalInfoDTO>(personalInfo);

                return personalInfoDTO;
            }
        }

        public async Task<PersonalInfoDTO> SavePersonalInfo(PersonalInfoDTO personalInfoDTO, long UserId)
        {
            var personalInfo = _mapper.Map<PersonalInfo>(personalInfoDTO);
            personalInfo.ApplicationUserId = UserId;

            var response = personalInfoDTO.Id==0 ? 
                await _personalInfoRepository.AddAsync(personalInfo) : 
                await _personalInfoRepository.UpdateAsync(personalInfo);

            return _mapper.Map<PersonalInfoDTO>(response);
        }
    }
}
