
using System;

namespace Api.ViewModels.Customer.Response
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PhoneNumber { get; set; }

        public string AuthorizedPersonName { get; set; }

        public double RemainingBalance => DebtBalance - ReceivableBalance;

        public double DebtBalance { get; set; }

        public double ReceivableBalance { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
