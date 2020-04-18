using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.WorkoutEntities
{
    public class Workout : BaseEntity
    {
        [Range(0, 14, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Order { get; set; }
        [Range(4, 180, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int EstimatedMin { get; set; }
        [MaxLength(450)]
        public string WorkoutComments { get; set; }
        public virtual IList<Exercise> Exercises { get; set; }
        public DateTime? WorkoutDate { get; set; }
        [ForeignKey(nameof(WoRoutine))]
        public long WoRoutineId { get; set; }        

    }
}
