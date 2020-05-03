using System;

namespace Dto
{
    public class Transaction : DtoBase
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } //navigation

        /// <summary>
        /// parameter.Id FK
        /// </summary>
        public int TypeId { get; set; }
        public virtual Parameter Type { get; set; } //navigation

        /// <summary>
        /// Skips the A- or B- chars in type name which indicates 'Alacak' or 'Borc'
        /// </summary>
        public string TypePrettyName => Type.Name.Substring(2);

        public double Amount { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// is 'Borclu' typed transaction? If false then it is "Alacakli' 
        /// </summary>
        public bool IsDebt { get; set; }

        public DateTime Date { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
