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
    }
}
