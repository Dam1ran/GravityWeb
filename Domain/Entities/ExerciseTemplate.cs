using Domain.Entities.WorkoutEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ExerciseTemplate : BaseEntity
    {
        [MaxLength(100)]
        [MinLength(4)]
        [Required]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Comments { get; set; }
        [MaxLength(10)]                
        public string Tempo { get; set; }
        public long? PrimaryMuscleId { get; set; }
        public long? SecondaryMuscleId { get; set; }
        public Muscle PrimaryMuscle { get; set; }
        public Muscle SecondaryMuscle { get; set; }
        public virtual IList<Exercise> Exercises { get; set; }
    }
}