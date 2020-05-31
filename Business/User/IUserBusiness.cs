
using System;
using Common.Response;

namespace Business.User
{
    public interface IUserBusiness
    {
        Response CreateDemoUserEntries(Guid userId, string lang);
    }
}
