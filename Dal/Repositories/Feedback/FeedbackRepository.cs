using Dal.Db;

namespace Dal.Repositories.Feedback
{
    public class FeedbackRepository : PostgreSqlDbRepository<Entities.Feedback, int>, IFeedbackRepository
    {
        public FeedbackRepository(BlackCoveredLedgerDbContext context) : base(context)
        {
        }
    }
}
