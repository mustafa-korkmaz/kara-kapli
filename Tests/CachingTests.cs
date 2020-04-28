using Xunit;
using System.Linq;
using Service.Caching;
using Business.Customer;

namespace Tests
{
    /// <summary>
    /// CachingMethodName_Should_ExpectedBehavior_When_StateUnderTest
    /// </summary>
    public class CachingTests : IClassFixture<Startup>
    {
        private readonly ICustomerBusiness _customerBusiness;

        public CachingTests(ICustomerBusiness customerBusiness, ICacheService cacheService)
        {
            _customerBusiness = customerBusiness;
        }

        [Fact]
        public void RefreshCache_Should_NotCallDatabaseTwice_WhenCacheNotRefreshed()
        {
            //arrange

            //act

            //first retrieve from db and cache blogs

            //assert
           // Assert.True(retrievedFromCache.Count() == retrievedFromDb.Count());
        }
    }
}
