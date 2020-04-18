using System.Collections.Generic;

namespace GravityDAL.DTO
{
    public class AppUserDTOsResponse
    {
        public List<AppUserDTO> appUserDTOs { get; set; }
        public int numberOfPages { get; set; }
        public int PageNumber { get; set; }
    }
}
