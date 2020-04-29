using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IGymSessionScheduleRepository : IRepository<GymSessionSchedule>
    {
        Task<IList<GymSessionSchedule>> GetByDayOfWeek(string dayOfWeek);

    }
}
