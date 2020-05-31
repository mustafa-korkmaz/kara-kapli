using System;
using Dal.Db;

namespace Dal.Repositories.User
{
    public class UserRepository : PostgreSqlDbRepository<Entities.Identity.ApplicationUser, Guid>, IUserRepository
    {
        public UserRepository(BlackCoveredLedgerDbContext context) : base(context)
        {

        }
    }
}
