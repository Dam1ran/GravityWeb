using System.ComponentModel.DataAnnotations;

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
