using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Dal.Db
{
    /// <summary>
    /// for ef migrations and updates
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BlackCoveredLedgerDbContext>
    {
        public BlackCoveredLedgerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlackCoveredLedgerDbContext>();
            optionsBuilder.UseNpgsql("Server=rogue.db.elephantsql.com;Port=5432;Database=mbxnorrc;User Id = mbxnorrc; Password=S4mD1oZZD-Re65jLb3oxYhI_qTWJ9L1F;CommandTimeout=20;");

            return new BlackCoveredLedgerDbContext(optionsBuilder.Options);
        }
    }
}
