using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PersonalClient : BaseEntity
    {
        [MaxLength(255)]
        [Required]
        public string Email { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public long ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
