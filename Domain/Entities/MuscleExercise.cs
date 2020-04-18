using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MuscleExercise : BaseEntity
    {
        public long MuscleId { get; set; }
        public virtual Muscle Muscle { get; set; }
        public long ExerciseTemplateId { get; set; }
        public virtual ExerciseTemplate ExerciseTemplate { get; set; }
    }
}
