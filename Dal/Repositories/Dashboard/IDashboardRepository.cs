
using System;

namespace Dal.Repositories.Dashboard
{
    public interface IDashboardRepository
    {
        Entities.Dashboard GetUserDashboard(Guid userId);
    }
}
