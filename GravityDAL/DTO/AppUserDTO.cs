using System;
using System.Collections.Generic;
using System.Text;

namespace GravityDAL.DTO
{
    public class AppUserDTO
    {
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userEmail { get; set; }
        public long coachId { get; set; }
        public long userRoleId { get; set; }
    }
}
