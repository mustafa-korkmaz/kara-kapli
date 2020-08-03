using System;
using System.Collections.Generic;
using System.Linq;

namespace Dto
{
    public class Dashboard
    {
        public int TransactionCount { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public double CustomerReceivablesTotal => Customers.Sum(c => c.ReceivableBalance);

        public double CustomerDebtsTotal => Customers.Sum(c => c.DebtBalance);

        public DateTime LastUpdatedAt { get; set; }
    }
}
