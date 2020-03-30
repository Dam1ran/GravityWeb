using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class PersonalInfo : BaseEntity
    {
        [MaxLength(255)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(255)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(6)]
        [Required]
        public string Gender { get; set; }

        [Range(7, 120)]
        public int Age { get; set; }

        [Range(100, 220)]
        public int HeightInCm { get; set; }

        [Range(30, 200)]
        public int WeightInKg { get; set; }

        [Range(0, 240)]
        public int FitnessExperienceInMonths { get; set; }

        public string OperationsAndFractures { get; set; }

        public string ChronicDiseases { get; set; }

        public string OtherThingsThatYourCoachShouldKnow { get; set; }

        public string TargetGoals { get; set; }
        
        [ForeignKey(nameof(ApplicationUser))]
        public long ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
