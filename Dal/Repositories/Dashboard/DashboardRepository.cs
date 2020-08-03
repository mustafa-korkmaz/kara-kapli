using Dal.Db;
using System;
using System.Linq;

namespace Dal.Repositories.Dashboard
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly BlackCoveredLedgerDbContext _context;

        public DashboardRepository(BlackCoveredLedgerDbContext context)
        {
            _context = context;
        }

        public Entities.Dashboard GetUserDashboard(Guid userId)
        {
            var transactions = _context.Set<Entities.Transaction>();

            var customers = _context.Set<Entities.Customer>();

            int transactionCount = transactions.Count(t => t.Customer.UserId == userId);

            var customerList = customers.Where(c => c.UserId == userId)
                .Select(c => new Entities.Customer
                {
                    Id = c.Id,
                    ReceivableBalance = c.ReceivableBalance,
                    DebtBalance = c.DebtBalance,
                })
                .ToList();

            return new Entities.Dashboard
            {
                Customers = customerList,
                TransactionCount = transactionCount
            };
        }
    }
}
