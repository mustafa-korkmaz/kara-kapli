using AutoMapper;
using Microsoft.Extensions.Logging;
using Dal;
using Common.Response;
using Common.Request;
using System.Collections.Generic;
using Common.Request.Criteria.Transaction;
using Dal.Repositories.Transaction;
using Business.Customer;
using Common;
using Business.Parameter;
using System.Linq;

namespace Business.Transaction
{
    public class TransactionBusiness : CrudBusiness<ITransactionRepository, Dal.Entities.Transaction, Dto.Transaction>, ITransactionBusiness
    {
        private readonly ICustomerBusiness _customerBusiness;
        private readonly IParameterBusiness _parameterBusiness;

        public TransactionBusiness(IUnitOfWork uow, ICustomerBusiness customerBusiness, IParameterBusiness parameterBusiness,
            ILogger<TransactionBusiness> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
            _customerBusiness = customerBusiness;
            _parameterBusiness = parameterBusiness;
            ValidateEntityOwner = true;
        }

        public PagedListResponse<Dto.Transaction> Search(FilteredPagedListRequest<SearchTransactionCriteria> request)
        {
            var resp = Repository.Search(request);

            var transactions = Mapper.Map<IEnumerable<Dal.Entities.Transaction>, IEnumerable<Dto.Transaction>>(resp.Items);

            SetTransactionTypes(transactions);

            return new PagedListResponse<Dto.Transaction>
            {
                Items = transactions,
                RecordsTotal = resp.RecordsTotal
            };
        }

        public override Response Add(Dto.Transaction dto)
        {
            var resp = _customerBusiness.Get(dto.CustomerId);

            if (resp.Type != ResponseType.Success)
            {
                return resp;
            }

            var customer = resp.Data;

            SetCustomerBalance(customer, dto.IsDebt, dto.Amount);

            customer.User = null; // prevent updating users table

            _customerBusiness.OwnerId = OwnerId;

            using (var tx = Uow.BeginTransaction())
            {
                //update customer's remaining balance 
                _customerBusiness.Edit(customer);

                base.Add(dto);

                tx.Commit();
            }

            return new Response
            {
                Type = ResponseType.Success
            };
        }

        public override DataResponse<int> Edit(Dto.Transaction dto)
        {
            var businessResp = new DataResponse<int>
            {
                Type = ResponseType.Fail
            };

            var entity = Repository.GetById(dto.Id);

            if (entity == null)
            {
                businessResp.ErrorCode = ErrorCode.RecordNotFound;
                return businessResp;
            }

            //do not let transaction customerId changes
            dto.CustomerId = entity.CustomerId;

            var resp = _customerBusiness.Get(entity.CustomerId);

            if (resp.Type != ResponseType.Success)
            {
                businessResp.ErrorCode = resp.ErrorCode;
                return businessResp;
            }

            var customer = resp.Data;

            //rollback the existing amount from customer's remaining balance
            DeleteCustomerBalance(customer, entity.IsDebt, entity.Amount);

            //now add the new balance to customer's remaining balance
            SetCustomerBalance(customer, dto.IsDebt, dto.Amount);

            _customerBusiness.OwnerId = OwnerId;

            using (var tx = Uow.BeginTransaction())
            {
                //update customer's remaining balance 
                _customerBusiness.Edit(customer);

                businessResp = base.Edit(dto);

                if (businessResp.Type != ResponseType.Success)
                {
                    return businessResp;
                }

                tx.Commit();
            }

            businessResp.Type = ResponseType.Success;
            return businessResp;
        }

        public override Response Delete(int id)
        {
            var deleteResp = new Response
            {
                Type = ResponseType.Fail
            };

            var entity = Repository.GetById(id);

            var resp = _customerBusiness.Get(entity.CustomerId);

            if (resp.Type != ResponseType.Success)
            {
                return resp;
            }

            var customer = resp.Data;

            //rollback the existing amount from customer's remaining balance
            DeleteCustomerBalance(customer, entity.IsDebt, entity.Amount);

            _customerBusiness.OwnerId = OwnerId;

            using (var tx = Uow.BeginTransaction())
            {
                //update customer's remaining balance 
                _customerBusiness.Edit(customer);

                deleteResp = base.Delete(id);

                if (deleteResp.Type != ResponseType.Success)
                {
                    return deleteResp;
                }

                tx.Commit();
            }

            return deleteResp;
        }

        protected override bool IsEntityOwnerValid(Dal.Entities.Transaction entity)
        {
            return entity.Customer.UserId.Equals(OwnerId);
        }

        private void SetTransactionTypes(IEnumerable<Dto.Transaction> transactions)
        {
            var parameters = _parameterBusiness.GetUserParameters(OwnerId);

            foreach (var item in transactions)
            {
                item.Type = parameters.FirstOrDefault(p => p.Id == item.TypeId);
            }
        }

        private void DeleteCustomerBalance(Dto.Customer customer, bool isDebt, double amount)
        {
            if (isDebt)
            {
                customer.DebtBalance -= amount;

            }
            else
            {
                customer.ReceivableBalance -= amount;
            }

        }

        private void SetCustomerBalance(Dto.Customer customer, bool isDebt, double amount)
        {
            if (isDebt)
            {
                customer.DebtBalance += amount;
            }
            else
            {
                customer.ReceivableBalance += amount;
            }

        }

        public bool IsDebtTransaction(int typeId)
        {
            var userParameters = _parameterBusiness.GetUserParameters(OwnerId);

            var transactionType = userParameters.First(p => p.Id == typeId);

            return transactionType.ParameterTypeId == DatabaseKeys.ParameterTypeId.Debt;
        }
    }
}
