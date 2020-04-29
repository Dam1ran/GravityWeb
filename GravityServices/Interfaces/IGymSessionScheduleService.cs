using Domain.Entities;
using GravityDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IGymSessionScheduleService
    {
        Task<GymSessionSchedule> SaveAsync(string WRpath,string uploadFolderName, ScheduleUploadData scheduleUploadData);
        Task<IList<GymSessionSchedule>> GetByDayOfWeek(string dayOfWeek, string envWebRootPath, string uploadFolderName);

    }
}
