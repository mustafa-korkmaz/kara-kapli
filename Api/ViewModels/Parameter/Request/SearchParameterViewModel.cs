using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels.Parameter.Request
{
    public class SearchParameterViewModel : PagedListViewModel
    {
        [StringLength(100, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = AppConstant.MinimumLengthForSearch)]
        [Display(Name = "PARAMETER_NAME")]
        [SnakeCaseQuery(nameof(Name))]
        public string Name { get; set; }
    }
}
