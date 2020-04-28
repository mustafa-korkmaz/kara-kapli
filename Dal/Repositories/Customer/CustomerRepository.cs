using Dal.Db;

namespace Dal.Repositories.Customer
{
    public class CustomerRepository : PostgreSqlDbRepository<Entities.Customer>, ICustomerRepository
    {
        public CustomerRepository(BlackCoveredLedgerDbContext context) : base(context)
        {
        }

        //public IEnumerable<Entities.Customer> SearchCustomers(string title)
        //{
        //    var query = Entities.Where(p => p.Title.Contains(title));

        //    return query.ToList();
        //}
    }
}
