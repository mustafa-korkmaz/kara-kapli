using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels.User.Request
{
    public class GetTokenViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name ="EMAIL_OR_USERNAME")]
        public string EmailOrUsername { get; set; }
      
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "PASSWORD")]
        public string Password { get; set; }
    }
}
