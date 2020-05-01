
using Common.Request;
using Common.Request.Criteria.Parameter;
using Common.Response;

namespace Business.Parameter
{
    public interface IParameterBusiness : ICrudBusiness<Dto.Parameter>
    {
        PagedListResponse<Dto.Parameter> Search(FilteredPagedListRequest<SearchParameterCriteria> request);
    }
}
