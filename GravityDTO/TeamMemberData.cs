using Microsoft.AspNetCore.Http;

namespace GravityDTO
{
    public class TeamMemberData
    {
        public IFormFile avatarFile { get; set; }
        public IFormFile imageFile { get; set; }
        public string fullName { get; set; }
        public string activity { get; set; }
        public string description { get; set; }
        public string moto { get; set; }
    }
}
