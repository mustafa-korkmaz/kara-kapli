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
        private ICustomerBusiness _customerBusiness;
        private IParameterBusiness _parameterBusiness;

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

        public override ResponseBase Add(Dto.Transaction dto)
        {
            var resp = _customerBusiness.Get(dto.CustomerId);

            if (resp.Type != ResponseType.Success)
            {
                return resp;
            }

            var customer = resp.Data;
            customer.RemainingBalance += GetBalance(dto);

            _customerBusiness.OwnerId = OwnerId;

            using (var tx = Uow.BeginTransaction())
            {
                //update customer's remaining balance 
                _customerBusiness.Edit(customer);

                base.Add(dto);

                tx.Commit();
            }

            return new ResponseBase
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

            var resp = _customerBusiness.Get(dto.CustomerId);

            if (resp.Type != ResponseType.Success)
            {
                businessResp.ErrorCode = resp.ErrorCode;
                return businessResp;
            }

            var entity = Repository.GetById(dto.Id);

            if (entity == null)
            {
                businessResp.ErrorCode = ErrorCode.RecordNotFound;
                return businessResp;
            }

            var customer = resp.Data;

            //first rollback the existing amount from customer's remaining balance
            customer.RemainingBalance += GetBalance(entity);

            //now add the new balance to customer's remaining balance
            customer.RemainingBalance += GetBalance(dto);

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

        private double GetBalance(Dto.Transaction t)
        {
            return t.IsReceivable ? t.Amount : -1 * t.Amount;
        }
        private double GetBalance(Dal.Entities.Transaction t)
        {
            return t.IsReceivable ? t.Amount : -1 * t.Amount;
        }

        private void SetTransactionTypes(IEnumerable<Dto.Transaction> transactions)
        {
            var parameters = _parameterBusiness.GetUserParameters(OwnerId);

            foreach (var item in transactions)
            {
                item.Type = parameters.FirstOrDefault(p => p.Id == item.TypeId);
            }
        }
    }
}
