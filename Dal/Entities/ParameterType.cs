using System.ComponentModel.DataAnnotations;

namespace Dal.Entities
{
    public class ParameterType : EntityBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
