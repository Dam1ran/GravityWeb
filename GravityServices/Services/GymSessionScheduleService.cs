using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using GravityServices.Utils;

namespace GravityServices.Services
{
    public class GymSessionScheduleService : IGymSessionScheduleService
    {
        private readonly IFileSaver _fileSaver;
        private readonly IGymSessionScheduleRepository _gymSessionScheduleRepository;

        public GymSessionScheduleService(
            IGymSessionScheduleRepository gymSessionScheduleRepository,
            IFileSaver fileSaver
            )
        {
            _gymSessionScheduleRepository = gymSessionScheduleRepository;
            _fileSaver = fileSaver;
        }

        public async Task<IList<GymSessionSchedule>> GetByDayOfWeekAsync(string dayOfWeek, string baseUrl, string uploadFolderName)
        {
            var result = await _gymSessionScheduleRepository.GetByDayOfWeekAsync(dayOfWeek);

            foreach (var item in result)
            {
                item.ImageUrl = item.ImageUrl.MakeUrl(baseUrl, uploadFolderName);
            }

            return result;
        }

        public async Task<GymSessionSchedule> SaveAsync(string WRpath, string uploadFolderName, ScheduleUploadData scheduleUploadData)
        {
            var response = await _fileSaver.SaveAsync(WRpath, uploadFolderName, scheduleUploadData.file);

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

                return await _gymSessionScheduleRepository.AddAsync(schedule);               
            }

            return null;

        }
    }
}
