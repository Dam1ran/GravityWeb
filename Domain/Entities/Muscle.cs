using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Muscle : BaseEntity
    {
        [MaxLength(100)]
        [MinLength(3)]
        [Required]
        public string Name { get; set; }       
    }
}
