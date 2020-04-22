using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{   
    public class AppUserCoach : BaseEntity
    {
        public long CoachId { get; set; }
        public virtual ApplicationUser Coach { get; set; }
        public long ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }    
}
