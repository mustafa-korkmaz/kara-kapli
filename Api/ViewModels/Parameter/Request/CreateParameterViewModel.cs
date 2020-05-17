using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels.Parameter.Request
{
    public class CreateParameterViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(100, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = AppConstant.MinimumLengthForSearch)]
        [Display(Name = "NAME")]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "PARAMETER_TYPE_ID")]
        [SnakeCaseQuery(nameof(ParameterTypeId))]
        public int? ParameterTypeId { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Range(0, byte.MaxValue, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "ORDER")]
        [SnakeCaseQuery(nameof(Order))]
        public byte? Order { get; set; }
    }
}