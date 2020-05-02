using Xunit;
using System.Linq;
using Service.Caching;
using Business.Customer;
using Common;
using System.Collections.Generic;
using System.Reflection;
using System;

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
        public void CacheKey_Should_Be_Fetched_When_MethodIsGiven()
        {
            //arrange
            var c = this;

            Action action = c.CacheKey_Should_Be_Fetched_When_MethodIsGiven;
            Func<int, string> func = c.MyTestMethod;

            //act
            var actionCacheKey = Utility.GetMethodResultCacheKey(action, new List<string> { "test1", "test2" });
            var funcCacheKey = Utility.GetMethodResultCacheKey(func, new List<object> { 1000 });

            //assert
            Assert.Equal("Tests.CachingTests.CacheKey_Should_Be_Fetched_When_MethodIsGiven(test1, test2)", actionCacheKey);
            Assert.Equal("Tests.CachingTests.MyTestMethod(1000)", funcCacheKey);
        }

        private string MyTestMethod(int a)
        {
            return a.ToString();
        }
    }
}
