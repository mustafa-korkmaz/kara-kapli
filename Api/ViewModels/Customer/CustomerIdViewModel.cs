
using Common;
using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.Customer
{
    public class CustomerIdViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "ID")]
        [SnakeCaseRoute(nameof(Id))]
        public int Id { get; set; }
    }
}