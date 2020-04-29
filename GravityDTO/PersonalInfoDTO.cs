using System;
using System.ComponentModel.DataAnnotations;

namespace GravityDTO
{
    public class PersonalInfoDTO
    {
        [Required]
        public long Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(255)]
        [Required]
        public string LastName { get; set; }

        [Required]
        [MaxLength(6)]
        public string Gender { get; set; }

        [Required]
        [Range(7, 120)]
        public int Age { get; set; }

        [Required]
        [Range(100, 220)]
        public int HeightInCm { get; set; }

        [Required]
        [Range(30, 200)]
        public int WeightInKg { get; set; }

        [Required]
        [Range(0, 240)]
        public int FitnessExperienceInMonths { get; set; }
        public string OperationsAndFractures { get; set; }
        public string ChronicDiseases { get; set; }
        public string OtherThingsThatYourCoachShouldKnow { get; set; }
        public string TargetGoals { get; set; }
    }
}
