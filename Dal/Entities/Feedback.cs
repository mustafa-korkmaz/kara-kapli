using System.ComponentModel.DataAnnotations;

namespace Dal.Entities
{
    public class Feedback : EntityBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; }
    }
}
