using AutoMapper;
using Dal.Repositories.Customer;
using Microsoft.Extensions.Logging;
using Dal;
using Common.Response;
using Common.Request;
using Common.Request.Criteria.Customer;
using System.Collections.Generic;

namespace Business.Customer
{
    public class CustomerOperationBusiness : CrudBusiness<ICustomerOperationRepository, Dal.Entities.Transaction, Dto.Transaction>, ICustomerOperationBusiness
    {
        public CustomerOperationBusiness(IUnitOfWork uow, ILogger<CustomerOperationBusiness> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
        }

        public PagedListResponse<Dto.Transaction> Search(FilteredPagedListRequest<SearchCustomerOperationCriteria> request)
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
