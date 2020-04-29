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
        public IList<WorkoutDTO> Workouts { get; set; }
    }
}
