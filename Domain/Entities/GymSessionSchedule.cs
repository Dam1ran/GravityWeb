using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class GymSessionSchedule : BaseEntity
    {
        public string DayOfWeek { get; set; }
        public string Time { get; set; }
        public string Practice { get; set; }
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }

    }
}
