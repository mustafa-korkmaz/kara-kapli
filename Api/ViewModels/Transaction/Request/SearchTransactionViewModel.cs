using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels.Transaction.Request
{
    public class SearchTransactionViewModel : PagedListViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "CUSTOMER_ID")]
        [SnakeCaseQuery(nameof(CustomerId))]
        public int? CustomerId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "TYPE_ID")]
        [SnakeCaseQuery(nameof(TypeId))]
        public int? TypeId { get; set; }

        [SnakeCaseQuery(nameof(IsDebt))]
        public bool? IsDebt { get; set; }
      
    }
}
