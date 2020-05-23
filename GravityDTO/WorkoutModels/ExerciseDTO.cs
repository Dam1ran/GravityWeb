using System.Collections.Generic;

namespace GravityDTO.WorkoutModels
{
    public class ExerciseDTO
    {        
        public long Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public long? ExerciseTemplateId { get; set; }                
        public long WorkoutId { get; set; }
        public IList<SetDTO> Sets { get; set; }

    }
}
