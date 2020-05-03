
using Common.Request;
using Common.Request.Criteria.Transaction;
using Common.Response;

namespace Business.Transaction
{
    public interface ITransactionBusiness : ICrudBusiness<Dto.Transaction>
    {
        PagedListResponse<Dto.Transaction> Search(FilteredPagedListRequest<SearchTransactionCriteria> request);

        /// <summary>
        /// indicates whether the trx is debt or receivable by typeId
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        bool IsDebtTransaction(int typeId);
    }
}
