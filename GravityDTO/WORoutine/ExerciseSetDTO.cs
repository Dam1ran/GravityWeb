using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GravityDTO.WORoutine
{
    public class ExerciseSetDTO
    {
        public long Id { get; set; }
        public long ExerciseId { get; set; }
        [Range(0, 11, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Order { get; set; }
        [Range(0, 301, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int? RestSecondsBetweenSet { get; set; }
        [Range(-1, 51, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int? NumberOfReps { get; set; }
        [Range(-1, 1001, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public float? Weight { get; set; }
        [Range(-1, 11, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int? RPE { get; set; }
    }
}
