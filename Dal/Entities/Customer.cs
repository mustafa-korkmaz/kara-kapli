using System;
using System.ComponentModel.DataAnnotations;

namespace Dal.Entities
{
    public class Customer : EntityBase
    {
        public Guid UserId { get; set; }
        public virtual Identity.ApplicationUser User { get; set; } // navigation 

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string AuthorizedPersonName { get; set; }

        public double RemainingBalance { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
