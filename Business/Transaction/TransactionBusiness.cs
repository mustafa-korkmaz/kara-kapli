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

namespace Business.Transaction
{
    public class TransactionBusiness : CrudBusiness<ITransactionRepository, Dal.Entities.Transaction, Dto.Transaction>, ITransactionBusiness
    {
        ICustomerBusiness _customerBusiness;

        public TransactionBusiness(IUnitOfWork uow, ICustomerBusiness customerBusiness, ILogger<TransactionBusiness> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
            _customerBusiness = customerBusiness;
            ValidateEntityOwner = true;
        }

        public PagedListResponse<Dto.Transaction> Search(FilteredPagedListRequest<SearchTransactionCriteria> request)
        {
            var resp = Repository.Search(request);

            var parameters = Mapper.Map<IEnumerable<Dal.Entities.Transaction>, IEnumerable<Dto.Transaction>>(resp.Items);

            return new PagedListResponse<Dto.Transaction>
            {
                Items = parameters,
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
    }
}
