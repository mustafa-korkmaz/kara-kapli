using System;
using System.ComponentModel.DataAnnotations;

namespace Dal.Entities
{
    public class Transaction : EntityBase
    {
        [Required]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } //navigation

        /// <summary>
        /// parameter.Id FK
        /// </summary>
        [Required]
        public int TypeId { get; set; }
        public virtual Parameter Type { get; set; } //navigation

        [Required]
        public double Amount { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        /// <summary>
        /// is 'Borclu' typed transaction? If false then it is "Alacakli' 
        /// </summary>
        [Required]
        public bool IsDebt { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }
    }
}
