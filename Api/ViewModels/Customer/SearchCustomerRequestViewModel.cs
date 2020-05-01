
using Api.ViewModels;
using Common;
using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.Customer
{
    public class SearchCustomerRequestViewModel : PagedListRequestViewModel
    {
        [StringLength(100, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = AppConstant.MinimumLengthForSearch)]
        [Display(Name = "TITLE")]
        [SnakeCaseQuery(nameof(Title))]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = AppConstant.MinimumLengthForSearch)]
        [Display(Name = "AUTHORIZED_PERSON_NAME")]
        [SnakeCaseQuery(nameof(AuthorizedPersonName))]
        public string AuthorizedPersonName { get; set; }
    }
}
