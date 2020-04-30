using System;
using System.ComponentModel.DataAnnotations;

namespace Dal.Entities
{
    public class Parameter : EntityBase
    {
        /// <summary>
        /// parameterTypes.Id FK
        /// </summary>
        [Required]
        public int ParameterTypeId { get; set; }
        public virtual ParameterType ParameterType { get; set; } //navigation

        [Required]
        public Guid UserId { get; set; }
        public virtual Identity.ApplicationUser User { get; set; } // navigation 

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public byte Order { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
