using Domain.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ApplicationUser : User
    {       
        public PersonalInfo PersonalInfo { get; set; }
        public IList<UserRole> Roles { get; set; }
        public AppUserCoach Coach { get; set; }        
        public IList<AppUserCoach> PersonalClients { get; set; }        

        //public virtual IList<WOProgramm> WOProgramms { get; set; }
    }
}
