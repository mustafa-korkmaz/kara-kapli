
using Api.ViewModels;
using Common;
using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.CustomerOperation
{
    public class SearchCustomerOperationRequestViewModel : PagedListRequestViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "CUSTOMER_ID")]
        public int? CustomerId { get; set; }

        public bool? IsReceivable { get; set; }

        /// <summary>
        /// IsPaid for Debt
        /// IsCollected for Receivable
        /// </summary>
        public bool? IsCompleted { get; set; }
    }
}
