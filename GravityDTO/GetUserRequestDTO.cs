using System;
using System.Collections.Generic;
using System.Text;

namespace GravityDTO
{
    public class GetUserRequestDTO
    {
        public string filter { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
