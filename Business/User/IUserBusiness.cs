
using System;
using Common.Response;

namespace Business.User
{
    public interface IUserBusiness
    {
        Response CreateDemoUserDefaultEntries(Guid userId, string lang);

        Response CreateUserDefaultEntries(Guid userId, string lang);
    }
}
