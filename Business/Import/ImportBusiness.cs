using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Business.Customer;
using Common;
using Common.Response;
using Dal;
using Dal.Repositories.Dashboard;
using Microsoft.Extensions.Logging;

namespace Business.Import
{
    public class ImportBusiness : IImportBusiness
    {
        private readonly ICustomerBusiness _customerBusiness;
        private readonly IMapper _mapper;
        private readonly ILogger<ImportBusiness> _logger;

        public ImportBusiness(IUnitOfWork uow, ILogger<ImportBusiness> logger, ICustomerBusiness customerBusiness,
            IMapper mapper)
        {
            _customerBusiness = customerBusiness;
            // _repository = uow.Repository<IDashboardRepository>();
            _logger = logger;
            _mapper = mapper;
        }

        public DataResponse<int> DoBasicImport(Dto.Customer[] customers)
        {
            var resp = new DataResponse<int>();

            var userId = customers.First().UserId;

            var validateResp = ValidateBasic(customers, userId);

            if (validateResp.Type != ResponseType.Success)
            {
                resp.Type = validateResp.Type;
                resp.ErrorCode = validateResp.ErrorCode;
                return resp;
            }

            return resp;
        }

        public DataResponse<int> DoDetailedImport(Dto.Customer[] customers)
        {
            throw new NotImplementedException();
        }

        private Response ValidateBasic(Dto.Customer[] customers, Guid userId)
        {
            var resp = new Response
            {
                Type = ResponseType.ValidationError
            };

            if (!HasUniqueCustomers(customers))
            {
                resp.ErrorCode = ErrorCode.CustomerTitleConflict;

                _logger.LogWarning($"Customer list is not unique for user {userId}");
                return resp;
            }

            var customerExistenceValidation = CustomersExist(customers, userId);

            if (customerExistenceValidation != string.Empty)
            {
                _logger.LogWarning($"{customerExistenceValidation} customer already exists for user {userId}");
                resp.ErrorCode = string.Format(ErrorCode.CustomerExists, customerExistenceValidation);
                return resp;
            }

            resp.Type = ResponseType.Success;
            return resp;
        }

        private bool HasUniqueCustomers(Dto.Customer[] customers)
        {
            var titles = customers.Select(c => c.Title).Distinct();

            return titles.Count() == customers.Length;
        }

        private string CustomersExist(Dto.Customer[] customers, Guid userId)
        {
            var result = string.Empty;

            var existingCustomers = _customerBusiness.GetAll(userId);

            if (existingCustomers == null)
            {
                return result;
            }

            var customerTitles = customers.Select(c => c.Title);
            var existingCustomerTitles = existingCustomers.Select(c => c.Title);

            var findings = from title in customerTitles
                           join existingTitle in existingCustomerTitles
                       on title equals existingTitle
                           select title;

            var array = findings as string[] ?? findings.ToArray();

            if (array.Any())
            {
                result = array.First();
            }

            return result;
        }

    }
}
