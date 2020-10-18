using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels.User.Request
{
    public class ResetViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name ="EMAIL_OR_USERNAME")]
        public string EmailOrUsername { get; set; }
    }
}
