using System;
using System.Collections.Generic;
using Common.Request;
using Common.Request.Criteria.Customer;
using Common.Response;

namespace Dal.Repositories.Customer
{
    public interface ICustomerRepository : IRepository<Entities.Customer>
    {
        PagedListResponse<Entities.Customer> Search(FilteredPagedListRequest<SearchCustomerCriteria> criteria);

        IEnumerable<Entities.Customer> GetAll(Guid userId);
    }
}
