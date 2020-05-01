using Common.Request;
using Common.Request.Criteria.Parameter;
using Common.Response;
using Dal.Db;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dal.Repositories.Parameter
{
    public class ParameterRepository : PostgreSqlDbRepository<Entities.Parameter>, IParameterRepository
    {
        public ParameterRepository(BlackCoveredLedgerDbContext context) : base(context)
        {

        }

        public PagedListResponse<Entities.Parameter> Search(FilteredPagedListRequest<SearchParameterCriteria> request)
        {
            var result = new PagedListResponse<Entities.Parameter>();

            var query = Entities.Where(p => p.UserId == request.FilterCriteria.UserId);

            if (string.IsNullOrEmpty(request.FilterCriteria.Name))
            {
                var nameLikeText = string.Format("%{0}%", request.FilterCriteria.Name);
                query = query.Where(p => EF.Functions.Like(p.Name, nameLikeText));
            }

            if (request.IncludeRecordsTotal)
            {
                result.RecordsTotal = query.Count();
            }

            result.Items = query
                .OrderBy(p => p.Order)
                .ThenBy(p => p.Name)
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToList();

            return result;
        }
    }
}
