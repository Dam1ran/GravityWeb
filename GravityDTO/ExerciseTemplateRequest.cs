using System;
using System.Collections.Generic;
using System.Text;

namespace GravityDTO
{
    public class ExerciseTemplateRequest
    {
        public string Filter { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
