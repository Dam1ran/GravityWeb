using Domain.Entities;
using GravityDTO;
using System.Threading.Tasks;

namespace GravityServices.Interfaces
{
    public interface IDayScheduleService
    {
        Task<GymSessionSchedule> SaveAsync(string WRpath, ScheduleUploadData scheduleUploadData);
    }
}
