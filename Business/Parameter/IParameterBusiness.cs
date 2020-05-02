
using Common.Request;
using Common.Request.Criteria.Parameter;
using Common.Response;
using System;
using System.Collections.Generic;

namespace Business.Parameter
{
    public interface IParameterBusiness : ICrudBusiness<Dto.Parameter>
    {
        PagedListResponse<Dto.Parameter> Search(FilteredPagedListRequest<SearchParameterCriteria> request);

        IEnumerable<Dto.Parameter> GetUserParameters(Guid userId);
    }
}
