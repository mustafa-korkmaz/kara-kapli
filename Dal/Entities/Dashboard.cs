using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal.Entities
{
    public class Dashboard : EntityBase
    {
        [NotMapped]
        public int TransactionCount { get; set; }

        [NotMapped]
        public ICollection Customers { get; set; }
    }
}
