using System;
using System.Collections.Generic;
using System.Text;

namespace GravityDTO
{
    public class PersonalInfoDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int HeightInCm { get; set; }
        public int WeightInKg { get; set; }
        public int FitnessExperienceInMonths { get; set; }
        public string OperationsAndFractures { get; set; }
        public string ChronicDiseases { get; set; }
        public string OtherThingsThatYourCoachShouldKnow { get; set; }
        public string TargetGoals { get; set; }
    }
}
