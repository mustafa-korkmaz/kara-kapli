
using System;
using Common.Response;

namespace Business.Import
{
    public interface IImportBusiness
    {
        DataResponse<int> DoBasicImport(Dto.Transaction[] transactions, Guid userId);

        DataResponse<int> DoDetailedImport(Dto.Transaction[] transactions, Guid userId);
    }
}
