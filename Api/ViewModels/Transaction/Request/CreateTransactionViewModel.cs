using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels.Transaction.Request
{
    public class CreateTransactionViewModel : UpdateTransactionViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "CUSTOMER_ID")]
        [SnakeCaseQuery(nameof(CustomerId))]
        public int CustomerId { get; set; }
    }
}