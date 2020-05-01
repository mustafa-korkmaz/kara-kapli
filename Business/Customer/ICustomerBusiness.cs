
using Common.Request;
using Common.Request.Criteria.Customer;
using Common.Response;

namespace Business.Customer
{
    public interface ICustomerBusiness : ICrudBusiness<Dto.Customer>
    {
        PagedListResponse<Dto.Customer> Search(FilteredPagedListRequest<SearchCustomerCriteria> request);
    }
}
