using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GravityDTO
{
    public class ScheduleUploadData
    {
        public IFormFile file { get; set; }
        public string dayOfWeek { get; set; }
        public string practice { get; set; }
        public string description { get; set; }
        public string hourMinute { get; set; }
    }
}
