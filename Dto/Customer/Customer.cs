
using Dto.User;
using System;
using System.Collections.Generic;

namespace Dto
{
    public class Customer : DtoBase
    {
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } // navigation 

        public string Title { get; set; }

        public string PhoneNumber { get; set; }

        public string AuthorizedPersonName { get; set; }

        public double DebtBalance { get; set; }

        public double ReceivableBalance { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }// 1=>n relation
    }
}
