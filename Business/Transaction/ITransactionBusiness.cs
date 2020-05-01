
using Common.Request;
using Common.Request.Criteria.Transaction;
using Common.Response;

namespace Business.Transaction
{
    public interface ITransactionBusiness : ICrudBusiness<Dto.Transaction>
    {
        PagedListResponse<Dto.Transaction> Search(FilteredPagedListRequest<SearchTransactionCriteria> request);
    }
}
