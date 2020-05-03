
using Common;
using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.Transaction
{
    public class SearchTransactionViewModel : PagedListViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "CUSTOMER_ID")]
        [SnakeCaseQuery(nameof(CustomerId))]
        public int? CustomerId { get; set; }

        [SnakeCaseQuery(nameof(IsDebt))]
        public bool? IsDebt { get; set; }
      
    }
}
