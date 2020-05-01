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

        public double Amount { get; set; }

        public string Description { get; set; }

        public bool IsReceivable { get; set; }

        /// <summary>
        /// IsPaid for Debt
        /// IsCollected for Receivable
        /// </summary>
        public bool IsCompleted { get; set; }

        public DateTime Date { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
