using AutoMapper;
using Dal.Repositories.Customer;
using Microsoft.Extensions.Logging;
using Dal;
using Common.Response;
using Common.Request;
using Common.Request.Criteria.Customer;
using System.Collections.Generic;

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
