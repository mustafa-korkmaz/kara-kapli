using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels.Customer.Request
{
    public class SearchCustomerViewModel : PagedListViewModel
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
