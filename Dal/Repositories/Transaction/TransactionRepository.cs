using Common.Request;
using Common.Request.Criteria.Transaction;
using Common.Response;
using Dal.Db;
using System.Linq;
using Common;

namespace Dal.Repositories.Transaction
{
    public class TransactionRepository : PostgreSqlDbRepository<Entities.Transaction, int>, ITransactionRepository
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

            if (request.FilterCriteria.TypeId.HasValue)
            {
                query = query.Where(p => p.TypeId == request.FilterCriteria.TypeId.Value);
            }

            if (request.FilterCriteria.IsDebt.HasValue)
            {
                query = query.Where(p => p.IsDebt == request.FilterCriteria.IsDebt.Value);
            }

            if (request.IncludeRecordsTotal)
            {
                result.RecordsTotal = query.Count();
            }

            switch (request.FilterCriteria.SortType)
            {
                case SortType.Ascending:
                    query = query
                        .OrderBy(p => p.Customer.Title);
                    break;
                case SortType.Descending:
                    query = query
                        .OrderByDescending(p => p.Customer.Title);
                    break;
                default:
                    query = query
                        .OrderByDescending(p => p.Id);
                    break;
            }

            query = query.Select(p => new Entities.Transaction
            {
                Id = p.Id,
                Description = p.Description,
                AttachmentName = p.AttachmentName,
                Amount = p.Amount,
                TypeId = p.TypeId,
                IsDebt = p.IsDebt,
                Date = p.Date,
                CreatedAt = p.CreatedAt,
                ModifiedAt = p.ModifiedAt,
                Customer = new Entities.Customer
                {
                    Id = p.CustomerId,
                    AuthorizedPersonName = p.Customer.AuthorizedPersonName,
                    Title = p.Customer.Title
                }
            });

            result.Items = query
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToList();

            return result;
        }
    }
}
