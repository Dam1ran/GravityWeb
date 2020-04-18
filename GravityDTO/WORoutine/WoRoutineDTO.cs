using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GravityDTO.WORoutine
{
    public class WoRoutineDTO
    {
        public long Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(1000)]
        [Required]
        public string Description { get; set; }
        //[Range(0, 15, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        //public int NumberOfWorkouts { get; set; }
        public IList<WorkoutDTO> Workouts { get; set; }

    }
}
