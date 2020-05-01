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
    public class CustomerBusiness : CrudBusiness<ICustomerRepository, Dal.Entities.Customer, Dto.Customer>, ICustomerBusiness
    {
        public CustomerBusiness(IUnitOfWork uow, ILogger<CustomerBusiness> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
        }

        public PagedListResponse<Dto.Customer> Search(FilteredPagedListRequest<SearchCustomerCriteria> request)
        {
            var resp = Repository.Search(request);

            var customers = Mapper.Map<IEnumerable<Dal.Entities.Customer>, IEnumerable<Dto.Customer>>(resp.Items);

            return new PagedListResponse<Dto.Customer>
            {
                Items = customers,
                RecordsTotal = resp.RecordsTotal
            };
        }
    }
}
