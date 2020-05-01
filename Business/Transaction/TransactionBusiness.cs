using AutoMapper;
using Microsoft.Extensions.Logging;
using Dal;
using Common.Response;
using Common.Request;
using System.Collections.Generic;
using Common.Request.Criteria.Transaction;
using Dal.Repositories.Transaction;

namespace Business.Transaction
{
    public class TransactionBusiness : CrudBusiness<ITransactionRepository, Dal.Entities.Transaction, Dto.Transaction>, ITransactionBusiness
    {
        public TransactionBusiness(IUnitOfWork uow, ILogger<TransactionBusiness> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
            ValidateEntityOwner = true;
        }

        public PagedListResponse<Dto.Transaction> Search(FilteredPagedListRequest<SearchTransactionCriteria> request)
        {
            var resp = Repository.Search(request);

            var parameters = Mapper.Map<IEnumerable<Dal.Entities.Transaction>, IEnumerable<Dto.Transaction>>(resp.Items);

            return new PagedListResponse<Dto.Transaction>
            {
                Items = parameters,
                RecordsTotal = resp.RecordsTotal
            };
        }
    }
}
