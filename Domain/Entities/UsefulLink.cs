using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class UsefulLink : BaseEntity
    {
        [MaxLength(200)]
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
