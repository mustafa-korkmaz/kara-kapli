using Common.Request;
using Common.Request.Criteria.Parameter;
using Common.Response;

namespace Dal.Repositories.Parameter
{
    public interface IParameterRepository : IRepository<Entities.Parameter>
    {
        PagedListResponse<Entities.Parameter> Search(FilteredPagedListRequest<SearchParameterCriteria> request);
    }
}
