
using Common.Request;
using Common.Request.Criteria.Customer;
using Common.Response;

namespace Business.Customer
{
    public interface ICustomerOperationBusiness : ICrudBusiness<Dto.CustomerOperation>
    {
        PagedListResponse<Dto.CustomerOperation> Search(FilteredPagedListRequest<SearchCustomerOperationCriteria> request);
    }
}
