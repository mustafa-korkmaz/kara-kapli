using Common.Request;
using Common.Request.Criteria.Transaction;
using Common.Response;
using Dal.Db;
using System.Linq;

namespace Dal.Repositories.Transaction
{
    public class TransactionRepository : PostgreSqlDbRepository<Entities.Transaction>, ITransactionRepository
    {
        public TransactionRepository(BlackCoveredLedgerDbContext context) : base(context)
        {

        }

        public PagedListResponse<Entities.Transaction> Search(FilteredPagedListRequest<SearchTransactionCriteria> request)
        {
            var result = new PagedListResponse<Entities.Transaction>();

            var query = Entities.Where(p => p.Customer.UserId == request.FilterCriteria.UserId);

            if (request.FilterCriteria.CustomerId.HasValue)
            {
                query = query.Where(p => p.CustomerId == request.FilterCriteria.CustomerId.Value);
            }

            if (request.FilterCriteria.IsReceivable.HasValue)
            {
                query = query.Where(p => p.IsReceivable == request.FilterCriteria.IsReceivable.Value);
            }

            if (request.FilterCriteria.IsCompleted.HasValue)
            {
                query = query.Where(p => p.IsCompleted == request.FilterCriteria.IsCompleted.Value);
            }

            if (request.IncludeRecordsTotal)
            {
                result.RecordsTotal = query.Count();
            }

            result.Items = query
                .OrderByDescending(p => p.Id)
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToList();

            return result;
        }
    }
}
