using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels.User.Request
{
    public class RegisterDemoViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name ="LANGUAGE")]
        public string Language { get; set; }
    }
}
