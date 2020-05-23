using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GravityDTO.WorkoutModels
{
    public class WorkoutDTO
    {
        public long Id { get; set; }
        [Range(0, 14, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Order { get; set; }
        [Range(4, 180, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int EstimatedMin { get; set; }
        [MaxLength(450)]
        public string WorkoutComments { get; set; }
        public DateTime? WorkoutDate { get; set; }
        public long RoutineId { get; set; }
        public IList<ExerciseDTO> Exercises { get; set; }

    }
}
