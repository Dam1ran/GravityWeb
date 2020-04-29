using System;

namespace GravityDTO.WORoutine
{
    public class ExerciseDTO
    {        
        public long Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public long? ExerciseTemplateId { get; set; }                
        public long WorkoutId { get; set; }
    }
}
