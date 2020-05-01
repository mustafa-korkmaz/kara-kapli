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
    public class CustomerOperationBusiness : CrudBusiness<ICustomerOperationRepository, Dal.Entities.CustomerOperation, Dto.CustomerOperation>, ICustomerOperationBusiness
    {
        public CustomerOperationBusiness(IUnitOfWork uow, ILogger<CustomerOperationBusiness> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
        }

        public PagedListResponse<Dto.CustomerOperation> Search(FilteredPagedListRequest<SearchCustomerOperationCriteria> request)
        {
            var resp = Repository.Search(request);

            var parameters = Mapper.Map<IEnumerable<Dal.Entities.CustomerOperation>, IEnumerable<Dto.CustomerOperation>>(resp.Items);

            return new PagedListResponse<Dto.CustomerOperation>
            {
                Items = parameters,
                RecordsTotal = resp.RecordsTotal
            };
        }
    }
}
