
using Common;
using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.CustomerOperation
{
    public class CreateCustomerOperationViewModel
    {
        public bool Title { get; set; }

        [StringLength(50, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = AppConstant.MinimumLengthForSearch)]
        [Display(Name = "AUTHORIZED_PERSON_NAME")]
        public string AuthorizedPersonName { get; set; }

        [StringLength(12, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = 10)]
        [Display(Name = "PHONE_NUMBER")]
        public string PhoneNumber { get; set; }
    }
}