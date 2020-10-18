using AutoMapper;
using Microsoft.Extensions.Logging;
using Dal;
using Dal.Repositories.Feedback;

namespace Business.Feedback
{
    public class FeedbackBusiness : CrudBusiness<IFeedbackRepository, Dal.Entities.Feedback, Dto.Feedback>, IFeedbackBusiness
    {
        public FeedbackBusiness(IUnitOfWork uow, ILogger<FeedbackBusiness> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
        }
    }
}
