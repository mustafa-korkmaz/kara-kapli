
using System;

namespace Common.Request.Criteria.Customer
{
    public class SearchCustomerCriteria
    {
        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string AuthorizedPersonName { get; set; }

        public SortType SortType { get; set; }
    }
}
