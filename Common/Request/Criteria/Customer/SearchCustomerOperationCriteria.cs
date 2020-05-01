
using System;

namespace Common.Request.Criteria.Customer
{
    public class SearchCustomerOperationCriteria
    {
        public Guid UserId { get; set; }

        public int? CustomerId { get; set; }

        public bool? IsReceivable { get; set; }

        /// <summary>
        /// IsPaid for Debt
        /// IsCollected for Receivable
        /// </summary>
        public bool? IsCompleted { get; set; }
    }
}
