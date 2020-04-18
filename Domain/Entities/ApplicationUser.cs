using Domain.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ApplicationUser : User
    {       
        public virtual PersonalInfo PersonalInfo { get; set; }
        public virtual IList<PersonalClient> Clients { get; set; }
        public virtual IList<UserRole> Roles { get; set; }

        //public virtual IList<WOProgramm> WOProgramms { get; set; }
    }
}
