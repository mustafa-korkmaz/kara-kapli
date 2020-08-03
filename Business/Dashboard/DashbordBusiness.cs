using System;
using System.Collections.Generic;
using AutoMapper;
using Common;
using Dal;
using Dal.Repositories.Dashboard;
using Microsoft.Extensions.Logging;
using Service.Caching;

namespace Business.Dashboard
{
    public class DashboardBusiness : IDashboardBusiness
    {
        private readonly ICacheService _cacheService;
        private readonly IDashboardRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DashboardBusiness> _logger;

        public DashboardBusiness(IUnitOfWork uow, ILogger<DashboardBusiness> logger, ICacheService cacheService,
            IMapper mapper)
        {
            _cacheService = cacheService;
            _repository = uow.Repository<IDashboardRepository>();
            _logger = logger;
            _mapper = mapper;
        }

        [CacheableResult(Provider = "LocalMemoryCacheService", ExpireInMinutes = 5)]
        public Dto.Dashboard GetUserDashboard(Guid userId)
        {
            var dashboard = _repository.GetUserDashboard(userId);

            var dto = _mapper.Map<Dal.Entities.Dashboard, Dto.Dashboard>(dashboard);

            dto.LastUpdatedAt = DateTime.UtcNow;

            return dto;
        }

        /// <summary>
        /// removes dashboard cache which belongs to User
        /// </summary>
        public void RefreshUserCache(Guid userId)
        {
            Func<Guid, Dto.Dashboard> info = GetUserDashboard;

            var list = new List<object> { userId };

            var cacheKey = Utility.GetMethodResultCacheKey(info, list);

            _cacheService.Remove(cacheKey);
        }
    }
}
