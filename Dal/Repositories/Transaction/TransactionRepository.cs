﻿using Common.Request;
using Common.Request.Criteria.Transaction;
using Common.Response;
using Dal.Db;
using System.Linq;

namespace Dal.Repositories.Transaction
{
    public class TransactionRepository : PostgreSqlDbRepository<Entities.Transaction>, ITransactionRepository
    {
        public TransactionRepository(BlackCoveredLedgerDbContext context) : base(context)
        {

        }

        public PagedListResponse<Entities.Transaction> Search(FilteredPagedListRequest<SearchTransactionCriteria> request)
        {
            var result = new PagedListResponse<Entities.Transaction>();

            var query = Entities.Where(p => p.Customer.UserId == request.FilterCriteria.UserId);

            if (request.FilterCriteria.CustomerId.HasValue)
            {
                query = query.Where(p => p.CustomerId == request.FilterCriteria.CustomerId.Value);
            }

            if (request.FilterCriteria.IsDebt.HasValue)
            {
                query = query.Where(p => p.IsDebt == request.FilterCriteria.IsDebt.Value);
            }

            if (request.IncludeRecordsTotal)
            {
                result.RecordsTotal = query.Count();
            }

            query = query.Select(p => new Entities.Transaction
            {
                Id = p.Id,
                Description = p.Description,
                Amount = p.Amount,
                TypeId = p.TypeId,
                IsDebt = p.IsDebt,
                Date = p.Date,
                CreatedAt = p.CreatedAt,
                ModifiedAt = p.ModifiedAt,
                Customer = new Entities.Customer
                {
                    Id = p.CustomerId,
                    AuthorizedPersonName = p.Customer.AuthorizedPersonName,
                    Title = p.Customer.Title
                }
            });

            result.Items = query
                .OrderByDescending(p => p.Id)
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToList();

            return result;
        }
    }
}
