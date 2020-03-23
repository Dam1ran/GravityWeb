using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class OurTeamMember : BaseEntity
    {
        [Required]
        public string AvatarUrl { get; set; }
        [Required]
        [MaxLength(30)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(20)]
        public string Activity { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }
        [Required]
        [MaxLength(200)]
        public string Moto { get; set; }
    }
}
