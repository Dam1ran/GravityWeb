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
    public class DayScheduleService : IDayScheduleService
    {
        private readonly IFileSaver _fileSaver;
        private readonly IGymSessionScheduleRepository _gymSessionScheduleRepository;

        public DayScheduleService(
            IGymSessionScheduleRepository gymSessionScheduleRepository,
            IFileSaver fileSaver
            )
        {
            _gymSessionScheduleRepository = gymSessionScheduleRepository;
            _fileSaver = fileSaver;
        }


        public async Task<GymSessionSchedule> SaveAsync(string WRpath,ScheduleUploadData scheduleUploadData)
        {
            var response = await _fileSaver.Save(WRpath, scheduleUploadData.file);

            if (response != "Failed")
            {
                var schedule = new GymSessionSchedule
                {
                    DayOfWeek = scheduleUploadData.dayOfWeek,
                    ImageUrl = response,
                    Description = scheduleUploadData.description,
                    Time = scheduleUploadData.hourMinute,
                    Practice = scheduleUploadData.practice
                };

                var dbSchedule = await _gymSessionScheduleRepository.AddAsync(schedule);

                if (dbSchedule != null)
                {
                    return dbSchedule;
                }

            }

            return null;

        }
    }
}
