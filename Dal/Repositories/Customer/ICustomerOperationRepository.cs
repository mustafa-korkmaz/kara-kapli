using Common.Request;
using Common.Request.Criteria.Customer;
using Common.Response;

namespace Dal.Repositories.Customer
{
    public interface ICustomerOperationRepository : IRepository<Entities.CustomerOperation>
    {
        PagedListResponse<Entities.CustomerOperation> Search(FilteredPagedListRequest<SearchCustomerOperationCriteria> request);
    }
}
