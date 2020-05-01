using Common.Request;
using Common.Request.Criteria.Transaction;
using Common.Response;

namespace Dal.Repositories.Transaction
{
    public interface ITransactionRepository : IRepository<Entities.Transaction>
    {
        PagedListResponse<Entities.Transaction> Search(FilteredPagedListRequest<SearchTransactionCriteria> request);
    }
}
