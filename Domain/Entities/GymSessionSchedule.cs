using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class GymSessionSchedule : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string HourMinute { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

    }
}
