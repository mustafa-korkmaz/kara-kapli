using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels.User.Request
{
    public class UpdateUserViewModel
    {
        [StringLength(100, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = AppConstant.MinimumLengthForSearch)]
        [Display(Name = "TITLE")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = AppConstant.MinimumLengthForSearch)]
        [Display(Name = "AUTHORIZED_PERSON_NAME")]
        public string AuthorizedPersonName { get; set; }
    }
}