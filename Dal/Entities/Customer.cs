using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dal.Entities
{
    public class Customer : EntityBase
    {
        [Required]
        public Guid UserId { get; set; }
        public virtual Identity.ApplicationUser User { get; set; } // navigation 

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(12)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        public string AuthorizedPersonName { get; set; }

        [Required]
        public double RemainingBalance { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }// 1=>n relation
    }
}
