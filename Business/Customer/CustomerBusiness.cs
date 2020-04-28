using System.Collections.Generic;
using AutoMapper;
using Dal.Repositories.Customer;
using Microsoft.Extensions.Logging;
using Dal;

namespace Business.Customer
{
    public class CustomerBusiness : CrudBusiness<ICustomerRepository, Dal.Entities.Customer, Dto.Post>, ICustomerBusiness
    {
        public CustomerBusiness(IUnitOfWork uow, ILogger<CustomerBusiness> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
        }

        //[CacheableResult(Provider = "LocalMemoryCacheService", ExpireInMinutes = 10)]
        public IEnumerable<Dto.Post> SearchPosts(string title)
        {
            return null;
            //Logger.LogInformation("searching blogs!");

            //var posts = Repository.SearchPosts(title);

            //var dtos = Mapper.Map<IEnumerable<Dal.Entities.Post>, IEnumerable<Dto.Post>>(posts);

            //return dtos;
        }
    }
}
