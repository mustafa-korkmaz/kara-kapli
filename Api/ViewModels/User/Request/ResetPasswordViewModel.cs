using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels.User.Request
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(50, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = 6)]
        [Display(Name = "PASSWORD")]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(1000, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "SECURITY_CODE")]
        public string SecurityCode { get; set; }
    }
}
