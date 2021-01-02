using System;
using System.Linq;
using Dal.Db;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories.User
{
    public class UserRepository : PostgreSqlDbRepository<Entities.Identity.ApplicationUser, Guid>, IUserRepository
    {
        public UserRepository(BlackCoveredLedgerDbContext context) : base(context)
        {

        }

        public Guid? GetUserIdByEmail(string email)
        {
            email = email.ToUpperInvariant();
            var user = Entities.AsNoTracking().FirstOrDefault(p => p.NormalizedEmail == email);

            return user?.Id;
        }
    }
}
