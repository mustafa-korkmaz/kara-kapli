using System.Collections.Generic;
using AutoMapper;
using Dal.Repositories.Customer;
using Microsoft.Extensions.Logging;
using Dal;
using Common.Response;
using Common.Request;
using Common.Request.Criteria.Parameter;
using Dal.Repositories.Parameter;
using System;
using Service.Caching;

namespace Business.Parameter
{
    public class ParameterBusiness : CrudBusiness<IParameterRepository, Dal.Entities.Parameter, Dto.Parameter>, IParameterBusiness
    {
        public ParameterBusiness(IUnitOfWork uow, ILogger<ParameterBusiness> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
        }

        public PagedListResponse<Dto.Parameter> Search(FilteredPagedListRequest<SearchParameterCriteria> request)
        {
            var resp = Repository.Search(request);

            var parameters = Mapper.Map<IEnumerable<Dal.Entities.Parameter>, IEnumerable<Dto.Parameter>>(resp.Items);

            return new PagedListResponse<Dto.Parameter>
            {
                Items = parameters,
                RecordsTotal = resp.RecordsTotal
            };
        }

        [CacheableResult(Provider = "LocalMemoryCacheService")]
        public IEnumerable<Dto.Parameter> GetUserParameters(Guid userId)
        {
            var entities = Repository.GetUserParameters(userId);

            var parameters = Mapper.Map<IEnumerable<Dal.Entities.Parameter>, IEnumerable<Dto.Parameter>>(entities);

            return parameters;
        }
    }
}
