using Common.Request;
using Common.Request.Criteria.Customer;
using Common.Response;
using Dal.Db;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dal.Repositories.Customer
{
    public class CustomerRepository : PostgreSqlDbRepository<Entities.Customer>, ICustomerRepository
    {
        public CustomerRepository(BlackCoveredLedgerDbContext context) : base(context)
        {
            
        }

        public PagedListResponse<Entities.Customer> Search(FilteredPagedListRequest<SearchCustomerCriteria> request)
        {
            var result = new PagedListResponse<Entities.Customer>();

            var query = Entities.Where(p => p.UserId == request.FilterCriteria.UserId);

            if (!string.IsNullOrEmpty(request.FilterCriteria.Title))
            {
                var titleLikeText = string.Format("%{0}%", request.FilterCriteria.Title);
                query = query.Where(p => EF.Functions.Like(p.Title, titleLikeText));
            }

            if (!string.IsNullOrEmpty(request.FilterCriteria.AuthorizedPersonName))
            {
                var personLikeText = string.Format("{0}%", request.FilterCriteria.AuthorizedPersonName);
                query = query.Where(p => EF.Functions.Like(p.Title, personLikeText));
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
