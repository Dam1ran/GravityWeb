using System;
using System.Collections.Generic;
using System.Text;

namespace GravityDTO
{
    public class UserDTOsResponse
    {
        public List<UserDTO> userDTOs { get; set; }
        public int numberOfPages { get; set; }
        public int PageNumber { get; set; }
    }
}
