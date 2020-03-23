using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IGymSessionScheduleRepository : IRepository<GymSessionSchedule>
    {
        Task<IList<GymSessionSchedule>> GetByDayOfWeek(string dayOfWeek);

    }
}
