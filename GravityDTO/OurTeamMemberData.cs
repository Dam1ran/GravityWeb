using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GravityDTO
{
    public class OurTeamMemberData
    {
        public IFormFile avatarFile { get; set; }
        public IFormFile imageFile { get; set; }
        public string fullName { get; set; }
        public string activity { get; set; }
        public string description { get; set; }
        public string moto { get; set; }
    }
}
