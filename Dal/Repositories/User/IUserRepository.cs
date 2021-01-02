
using System;

namespace Dal.Repositories.User
{
    public interface IUserRepository : IRepository<Entities.Identity.ApplicationUser>
    {
        Guid? GetUserIdByEmail(string email);
    }
}
