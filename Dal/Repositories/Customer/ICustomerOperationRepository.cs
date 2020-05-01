using Common.Request;
using Common.Request.Criteria.Customer;
using Common.Response;

namespace Dal.Repositories.Customer
{
    public interface ICustomerOperationRepository : IRepository<Entities.Transaction>
    {
        PagedListResponse<Entities.Transaction> Search(FilteredPagedListRequest<SearchCustomerOperationCriteria> request);
    }
}
