using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UsefulLink : BaseEntity
    {
        [MaxLength(200)]
        [Required]
        public string Description { get; set; }
        [Required]
        [Url]
        public string Link { get; set; }
    }
}
