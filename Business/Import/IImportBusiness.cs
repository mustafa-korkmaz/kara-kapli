
using Common.Response;

namespace Business.Import
{
    public interface IImportBusiness
    {
        DataResponse<int> DoBasicImport(Dto.Customer[] customers);

        DataResponse<int> DoDetailedImport(Dto.Customer[] customers);
    }
}
