using System.ComponentModel.DataAnnotations;

namespace GravityDTO
{
    public class ExerciseTemplateDTO
    {
        public long Id { get; set; }

        [MaxLength(100)]
        [MinLength(4)]
        [Required]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Comments { get; set; }

        [MaxLength(10)]        
        public string Tempo { get; set; }
        public long PrimaryMuscleId { get; set; }
        public long SecondaryMuscleId { get; set; }

    }
}
