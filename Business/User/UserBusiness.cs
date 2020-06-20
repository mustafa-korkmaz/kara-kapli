using System;
using System.Collections.Generic;
using AutoMapper;
using Business.Customer;
using Business.Parameter;
using Business.Transaction;
using Common;
using Common.Response;
using Microsoft.Extensions.Logging;
using Dal;
using Dal.Repositories.User;
using Dto.User;
using System.Text.Json;

namespace Business.User
{
    public class UserBusiness : IUserBusiness
    {
        private readonly ICustomerBusiness _customerBusiness;
        private readonly IParameterBusiness _parameterBusiness;
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserBusiness> _logger;

        public UserBusiness(IUnitOfWork uow, ILogger<UserBusiness> logger,
            ICustomerBusiness customerBusiness, ITransactionBusiness transactionBusiness,
            IParameterBusiness parameterBusiness, IMapper mapper)
        {
            _customerBusiness = customerBusiness;
            _parameterBusiness = parameterBusiness;
            _uow = uow;
            _repository = _uow.Repository<IUserRepository, Dal.Entities.Identity.ApplicationUser>();
            _logger = logger;
            _mapper = mapper;
        }

        public Response CreateDemoUserDefaultEntries(Guid userId, string lang)
        {
            var parameters = GetParameters(userId, lang);

            using (var tx = _uow.BeginTransaction())
            {
                //add parameters
                _parameterBusiness.AddRange(parameters);

                var customers = GetCustomers(userId, parameters, lang);

                SetCustomerRemainingBalances(customers);

                //add customers
                _customerBusiness.AddRange(customers);

                tx.Commit();
            }

            return new Response
            {
                Type = ResponseType.Success
            };
        }

        public Response CreateUserDefaultEntries(Guid userId, string lang)
        {
            var parameters = GetParameters(userId, lang);

            using (var tx = _uow.BeginTransaction())
            {
                //add parameters
                _parameterBusiness.AddRange(parameters);

                tx.Commit();
            }

            return new Response
            {
                Type = ResponseType.Success
            };
        }

        public UserSettings GetSettings(string settingsJson)
        {
           return JsonSerializer.Deserialize<UserSettings>(settingsJson);
        }

        private Dto.Parameter[] GetParameters(Guid userId, string lang)
        {
            var list = new List<Dto.Parameter>();

            switch (lang.ToLower())
            {
                case Language.Turkish:
                    list.AddRange(new List<Dto.Parameter>
                    {
                        new Dto.Parameter  {
                        Name = "Cariye Borç",
                        UserId = userId,
                        Order = 0,
                        ParameterTypeId = DatabaseKeys.ParameterTypeId.Debt
                        },
                        new Dto.Parameter  {
                            Name = "Cariye Alacak",
                            UserId = userId,
                            Order = 1,
                            ParameterTypeId = DatabaseKeys.ParameterTypeId.Receivable
                        },
                        new Dto.Parameter  {
                            Name = "Tahsilat",
                            UserId = userId,
                            Order = 2,
                            ParameterTypeId = DatabaseKeys.ParameterTypeId.Receivable
                        },
                        new Dto.Parameter  {
                            Name = "Ödeme",
                            UserId = userId,
                            Order = 3,
                            ParameterTypeId = DatabaseKeys.ParameterTypeId.Debt
                        }
                    });
                    break;
                default:
                    list.AddRange(new List<Dto.Parameter>
                    {
                        new Dto.Parameter  {
                            Name = "Customer Debt",
                            UserId = userId,
                            Order = 0,
                            ParameterTypeId = DatabaseKeys.ParameterTypeId.Debt
                        },
                        new Dto.Parameter  {
                            Name = "Customer Receivable",
                            UserId = userId,
                            Order = 1,
                            ParameterTypeId = DatabaseKeys.ParameterTypeId.Receivable
                        },
                        new Dto.Parameter  {
                            Name = "Collection",
                            UserId = userId,
                            Order = 2,
                            ParameterTypeId = DatabaseKeys.ParameterTypeId.Receivable
                        },
                        new Dto.Parameter  {
                            Name = "Payment",
                            UserId = userId,
                            Order = 3,
                            ParameterTypeId = DatabaseKeys.ParameterTypeId.Debt
                        }
                    });
                    break;
            }

            return list.ToArray();
        }

        private Dto.Customer[] GetCustomers(Guid userId, Dto.Parameter[] parameters, string lang)
        {
            var list = new List<Dto.Customer>();

            switch (lang.ToLower())
            {
                case Language.Turkish:
                    list.AddRange(new List<Dto.Customer>
                    {
                        new Dto.Customer  {
                        UserId = userId,
                        Transactions = GetTransactions(parameters,lang),
                        Title = "Korkmaz Ltd. Şti.",
                        AuthorizedPersonName = "Mustafa Korkmaz",
                        CreatedAt = DateTime.UtcNow
                        },
                        new Dto.Customer  {
                            UserId = userId,
                            Transactions = GetTransactions(parameters,lang),
                            Title = "Ahmet Faik",
                            AuthorizedPersonName = "Samed Faik",
                            CreatedAt = DateTime.UtcNow
                        },
                        new Dto.Customer {
                            UserId = userId,
                            Transactions = GetTransactions(parameters,lang),
                            Title = "Duran A.Ş.",
                            AuthorizedPersonName = "Ali Duran",
                            PhoneNumber = "5441234567",
                            CreatedAt = DateTime.UtcNow
                        },
                    });
                    break;
                default:
                    list.AddRange(new List<Dto.Customer>
                    {
                        new Dto.Customer  {
                            UserId = userId,
                            Transactions = GetTransactions(parameters,lang),
                            Title = "Acme Corp.",
                            AuthorizedPersonName = "Bugs Bunny",
                            CreatedAt = DateTime.UtcNow
                        },
                        new Dto.Customer  {
                            UserId = userId,
                            Transactions = GetTransactions(parameters,lang),
                            Title = "John Doe",
                            AuthorizedPersonName = "John",
                            PhoneNumber = "5551234567",
                            CreatedAt = DateTime.UtcNow
                        },
                        new Dto.Customer {
                            UserId = userId,
                            Transactions = GetTransactions(parameters,lang),
                            Title = "Pearson Spector Inc.",
                            AuthorizedPersonName = "Mike Ross",
                            CreatedAt = DateTime.UtcNow
                        }
                    });
                    break;
            }

            return list.ToArray();
        }

        private Dto.Transaction[] GetTransactions(Dto.Parameter[] parameters, string lang)
        {
            var list = new List<Dto.Transaction>();

            foreach (var parameter in parameters)
            {
                var desc = lang.ToLower() == Language.Turkish ? parameter.Name + " işlemi" : parameter.Name + " operation";

                list.Add(GetDemoTransaction(0, parameter.Id, parameter.ParameterTypeId == 2, desc));
            }

            return list.ToArray();
        }

        private Dto.Transaction GetDemoTransaction(int customerId, int typeId, bool isDebt, string desc)
        {
            var now = DateTime.UtcNow;
            Random rand = new Random();

            var amount = rand.NextDouble() + rand.Next(500, 10000);
            return new Dto.Transaction
            {
                Amount = Math.Round(amount, 2),
                CreatedAt = now,
                Date = DateTime.Today,
                IsDebt = isDebt,
                ModifiedAt = now,
                TypeId = typeId,
                CustomerId = customerId,
                Description = desc
            };
        }

        private void SetCustomerRemainingBalances(Dto.Customer[] customers)
        {
            foreach (var c in customers)
            {
                var list = c.Transactions;

                foreach (var t in list)
                {
                    if (t.IsDebt)
                    {
                        c.DebtBalance += t.Amount;
                    }
                    else
                    {
                        c.ReceivableBalance += t.Amount;
                    }
                }
            }
        }
    }
}
