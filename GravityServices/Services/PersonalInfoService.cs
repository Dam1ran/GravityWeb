﻿using AutoMapper;
using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Interfaces;
using System.Threading.Tasks;

namespace GravityServices.Services
{
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly IPersonalInfoRepository _personalInfoRepository;
        private readonly IMapper _mapper;

        public PersonalInfoService(
            IPersonalInfoRepository personalInfoRepository,
            IMapper mapper)
        {
            _personalInfoRepository = personalInfoRepository;
            _mapper = mapper;
        }

        public async Task<PersonalInfoDTO> GetByUserIdAsync(long Id)
        {
            var personalInfo = await _personalInfoRepository.GetPersonalInfoByUserIdAsync(Id);

            if (personalInfo == null)
            {               
                return new PersonalInfoDTO();
            }
            else
            {
                return _mapper.Map<PersonalInfoDTO>(personalInfo);
            }
        }

        public async Task<PersonalInfoDTO> SavePersonalInfoAsync(PersonalInfoDTO personalInfoDTO, long UserId)
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
