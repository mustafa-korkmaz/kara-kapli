
using Common;
using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.Parameter
{
    public class SearchParameterViewModel : PagedListViewModel
    {
        [StringLength(100, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = AppConstant.MinimumLengthForSearch)]
        [Display(Name = "NAME")]
        [SnakeCaseQuery(nameof(Name))]
        public string Name { get; set; }
    }
}
