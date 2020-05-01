
using Common.Request;
using Common.Request.Criteria.Customer;
using Common.Response;

namespace Business.Customer
{
    public interface ICustomerOperationBusiness : ICrudBusiness<Dto.Transaction>
    {
        PagedListResponse<Dto.Transaction> Search(FilteredPagedListRequest<SearchCustomerOperationCriteria> request);
    }
}
