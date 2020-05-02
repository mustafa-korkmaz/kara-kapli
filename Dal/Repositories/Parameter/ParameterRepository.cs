using Common.Request;
using Common.Request.Criteria.Parameter;
using Common.Response;
using Dal.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dal.Repositories.Parameter
{
    public class ParameterRepository : PostgreSqlDbRepository<Entities.Parameter>, IParameterRepository
    {
        public ParameterRepository(BlackCoveredLedgerDbContext context) : base(context)
        {

        }

        public IEnumerable<Entities.Parameter> GetUserParameters(Guid userId)
        {
            var query = Entities.Where(p => p.UserId == userId);

            return query.Select(p => new Entities.Parameter
            {
                Id = p.Id,
                Name = p.Name,
                ParameterTypeId = p.ParameterTypeId
            })
            .ToList();
        }

        public PagedListResponse<Entities.Parameter> Search(FilteredPagedListRequest<SearchParameterCriteria> request)
        {
            var result = new PagedListResponse<Entities.Parameter>();

            var query = Entities.Where(p => p.UserId == request.FilterCriteria.UserId && p.IsDeleted == false);

            if (!string.IsNullOrEmpty(request.FilterCriteria.Name))
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
