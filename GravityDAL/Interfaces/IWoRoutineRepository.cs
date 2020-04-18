using Domain.Entities.WorkoutEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GravityDAL.Interfaces
{
    public interface IWoRoutineRepository : IRepository<WoRoutine>
    {
        Task<WoRoutine> GetByIdWithInclude(long id);
    }
}
