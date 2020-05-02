
using Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.Transaction
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