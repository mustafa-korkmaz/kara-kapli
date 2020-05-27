
using System;

namespace Common.Request.Criteria.Transaction
{
    public class SearchTransactionCriteria
    {
        public Guid UserId { get; set; }

        public int? CustomerId { get; set; }

        public int? TypeId { get; set; }

        public bool? IsDebt { get; set; }
    }
}
