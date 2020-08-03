
using System;

namespace Business.Dashboard
{
    public interface IDashboardBusiness
    {
        Dto.Dashboard GetUserDashboard(Guid userId);

        void RefreshUserCache(Guid userId);
    }
}
