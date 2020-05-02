using Common.Request;
using Common.Request.Criteria.Parameter;
using Common.Response;
using System;
using System.Collections.Generic;

namespace Dal.Repositories.Parameter
{
    public interface IParameterRepository : IRepository<Entities.Parameter>
    {
        PagedListResponse<Entities.Parameter> Search(FilteredPagedListRequest<SearchParameterCriteria> request);

        IEnumerable<Entities.Parameter> GetUserParameters(Guid userId);
    }
}
