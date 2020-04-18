﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.WorkoutEntities
{
    public class Exercise : BaseEntity
    {
        [MaxLength(100)]
        [MinLength(4)]
        [Required]
        public string Name { get; set; }
        [Range(0, 31, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Order { get; set; }
        public long? ExerciseTemplateId { get; set; }
        public ExerciseTemplate ExerciseTemplate { get; set; }
        public Workout Workout { get; set; }
        public long WorkoutId { get; set; }
        public IList<ExerciseSet> ExerciseSets { get; set; }

    }
}
