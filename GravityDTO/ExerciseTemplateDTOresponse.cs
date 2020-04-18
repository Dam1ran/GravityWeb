using System.Collections.Generic;

namespace GravityDTO
{
    public class ExerciseTemplateDTOresponse
    {        
        public List<ExerciseTemplateDTO> ExerciseTemplateDTOs { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Length { get; set; }

    }
}
