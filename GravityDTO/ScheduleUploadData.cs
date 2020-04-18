using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GravityDTO
{
    public class ScheduleUploadData
    {
        [Required]
        public IFormFile file { get; set; }
        [Required]
        public string dayOfWeek { get; set; }
        [Required]
        public string practice { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string hourMinute { get; set; }
    }
}
