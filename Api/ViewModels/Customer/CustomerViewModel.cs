﻿
namespace Api.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PhoneNumber { get; set; }

        public string AuthorizedPersonName { get; set; }

        public double RemainingBalance { get; set; }

        public string CreatedAtText { get; set; }
    }
}
