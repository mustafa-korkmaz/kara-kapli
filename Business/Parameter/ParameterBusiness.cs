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
using Common;

namespace Business.Parameter
{
    public class ParameterBusiness : CrudBusiness<IParameterRepository, Dal.Entities.Parameter, Dto.Parameter>, IParameterBusiness
    {
        private readonly ICacheService _cacheService;

        public ParameterBusiness(IUnitOfWork uow, ICacheService cacheService, ILogger<ParameterBusiness> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
            _cacheService = cacheService;
            ValidateEntityOwner = true;
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

        public override Response Add(Dto.Parameter dto)
        {
            var resp = base.Add(dto);

            if (resp.Type != ResponseType.Success)
            {
                return resp;
            }

            RefreshUserParametersCache();

            return resp;
        }

        public override DataResponse<int> Edit(Dto.Parameter dto)
        {
            var resp = base.Edit(dto);

            if (resp.Type != ResponseType.Success)
            {
                return resp;
            }

            RefreshUserParametersCache();

            return resp;
        }

        public override Response SoftDelete(int id)
        {
            var resp = base.SoftDelete(id);

            if (resp.Type != ResponseType.Success)
            {
                return resp;
            }

            RefreshUserParametersCache();

            return resp;
        }

        /// <summary>
        /// remove parameters which belongs to Owner
        /// </summary>
        private void RefreshUserParametersCache()
        {
            var list = new List<object> { OwnerId };

            Func<Guid, IEnumerable<Dto.Parameter>> info = GetUserParameters;

            var cacheKey = Utility.GetMethodResultCacheKey(info, list);

            _cacheService.Remove(cacheKey);
        }
    }
}
