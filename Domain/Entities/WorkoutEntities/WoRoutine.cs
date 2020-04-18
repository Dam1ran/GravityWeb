﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.WorkoutEntities
{
    public class WoRoutine : BaseEntity
    {
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(1000)]
        [Required]
        public string Description { get; set; }
        //[Range(0, 15, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        //public int NumberOfWorkouts { get; set; }
        public virtual IList<Workout> Workouts { get; set; }

        //[ForeignKey(nameof(ApplicationUser))]
        //public long ApplicationUserId { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
