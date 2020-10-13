using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels.Customer.Request
{
    public class FeedbackViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "MESSAGE")]
        public string Message { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(50)]
        [Display(Name = "EMAIL")]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(100)]
        [Display(Name = "NAME")]
        public string Name { get; set; }
    }
}