using System.ComponentModel.DataAnnotations;
using Api.ViewModels.Customer.Request;
using Common;

namespace Api.ViewModels.Import.Request
{
    public class BasicDataImportViewModel
    {
        public CreateCustomerViewModel Customer { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "DEBT_BALANCE")]
        public double? DebtBalance { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "RECEIVABLE_BALANCE")]
        public double? ReceivableBalance { get; set; }
    }
}