
using Api.ViewModels;
using Common;
using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.CustomerOperation
{
    public class SearchCustomerOperationViewModel : PagedListViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "CUSTOMER_ID")]
        [SnakeCaseQuery(nameof(CustomerId))]
        public int? CustomerId { get; set; }

        [SnakeCaseQuery(nameof(IsReceivable))]
        public bool? IsReceivable { get; set; }

        /// <summary>
        /// IsPaid for Debt
        /// IsCollected for Receivable
        /// </summary>
        [SnakeCaseQuery(nameof(IsCompleted))]
        public bool? IsCompleted { get; set; }
    }
}
