using System;
using System.Collections.Generic;
using System.Linq;
using Business.Customer;
using Xunit;
using Business.Import;
using Common;
using Dto;

namespace Tests
{
    /// <summary>
    /// CachingMethodName_Should_ExpectedBehavior_When_StateUnderTest
    /// </summary>
    public class ImportTests : IClassFixture<Startup>
    {
        private readonly IImportBusiness _service;
        private readonly ICustomerBusiness _customerBusiness;

        public ImportTests(IImportBusiness service, ICustomerBusiness customerBusiness)
        {
            _service = service;
            _customerBusiness = customerBusiness;
        }

        [Fact]
        public void BasicImport_Should_GiveError_When_NotUniqueCustomerListIsGiven()
        {
            //arrange
            var userId = Guid.NewGuid();
            var customers = CreateBasicImportCustomersWithDuplicatedTitles(userId);

            //act
            var resp = _service.DoBasicImport(customers.ToArray());

            //assert
            Assert.True(resp.Type == ResponseType.ValidationError);
            Assert.True(resp.ErrorCode == ErrorCode.CustomerTitleConflict);
        }

        [Fact]
        public void BasicImport_Should_GiveError_When_AnyCustomerExists()
        {
            //arrange
            var userId = Guid.NewGuid();
            var customers = CreateBasicImportCustomersWithUniqueTitles(userId);

            CreateDatabaseCustomers(userId);

            //act
            var resp = _service.DoBasicImport(customers.ToArray());

            //assert
            Assert.True(resp.Type == ResponseType.ValidationError);
            Assert.Contains("CUSTOMER_EXISTS", resp.ErrorCode);
        }

        [Fact]
        public void BasicImport_Should_Work_When_ValidationsPass()
        {
            //arrange
            var userId = Guid.NewGuid();

            var customers = CreateBasicImportCustomersWithRandomTitles(userId);

            CreateDatabaseCustomers(userId);

            //act
            var resp = _service.DoBasicImport(customers.ToArray());

            //assert
            Assert.True(resp.Type == ResponseType.Success);
        }

        private IEnumerable<Customer> CreateBasicImportCustomersWithDuplicatedTitles(Guid userId)
        {
            var list = new List<Customer>();

            var customers = new string[] { "Customer 1", "Customer 2", "Customer 3", "Customer 4", "Customer 5" };

            for (int i = 0; i < 100; i++)
            {
                var now = DateTime.UtcNow;
                Random rand = new Random();

                var amount = rand.NextDouble() + rand.Next(500, 10000);

                var c = new Customer
                {
                    UserId = userId,
                    Title = customers[i % 5],
                    AuthorizedPersonName = "Test" + i,
                    Transactions = new List<Transaction>
                    {
                        new Transaction
                        {
                            Amount = Math.Round(amount, 2),
                            CreatedAt = now,
                            Date = DateTime.Today,
                            IsDebt = false,
                            ModifiedAt = now,
                        }
                    }
                };

                list.Add(c);
            }

            return list;
        }

        private IEnumerable<Customer> CreateBasicImportCustomersWithUniqueTitles(Guid userId)
        {
            var list = new List<Customer>();

            var customers = new string[100];

            for (int i = 0; i < 100; i++)
            {
                customers[i] = "Customer " + i;
                var now = DateTime.UtcNow;
                Random rand = new Random();

                var amount = rand.NextDouble() + rand.Next(500, 10000);

                var c = new Customer
                {
                    UserId = userId,
                    Title = customers[i],
                    AuthorizedPersonName = "Test" + i,
                    Transactions = new List<Transaction>
                    {
                        new Transaction
                        {
                            Amount = Math.Round(amount, 2),
                            CreatedAt = now,
                            Date = DateTime.Today,
                            IsDebt = false,
                            ModifiedAt = now,
                        }
                    }
                };

                list.Add(c);
            }

            return list;
        }

        private IEnumerable<Customer> CreateBasicImportCustomersWithRandomTitles(Guid userId)
        {
            var list = new List<Customer>();

            var customers = new string[100];

            for (int i = 0; i < 100; i++)
            {
                customers[i] = Guid.NewGuid().ToString();
                var now = DateTime.UtcNow;
                Random rand = new Random();

                var amount = rand.NextDouble() + rand.Next(500, 10000);

                var c = new Customer
                {
                    UserId = userId,
                    Title = customers[i],
                    AuthorizedPersonName = "Test" + i,
                    Transactions = new List<Transaction>
                    {
                        new Transaction
                        {
                            Amount = Math.Round(amount, 2),
                            CreatedAt = now,
                            Date = DateTime.Today,
                            IsDebt = false,
                            ModifiedAt = now,
                        }
                    }
                };

                list.Add(c);
            }

            return list;
        }

        private void CreateDatabaseCustomers(Guid userId)
        {
            var customers = new List<Customer>();

            for (int i = 0; i < 100; i++)
            {
                var title = "Customer " + i;
                Random rand = new Random();

                customers.Add(new Customer
                {
                    Id = 1000 + i,
                    UserId = userId,
                    Title = title,
                    AuthorizedPersonName = title
                });
            }

            _customerBusiness.AddRange(customers.ToArray());
        }

    }
}
