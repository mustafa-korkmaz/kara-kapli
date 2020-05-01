﻿
using Common;
using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels.Customer
{
    public class CreateCustomerRequestViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(100, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = AppConstant.MinimumLengthForSearch)]
        [Display(Name = "TITLE")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = AppConstant.MinimumLengthForSearch)]
        [Display(Name = "AUTHORIZED_PERSON_NAME")]
        public string AuthorizedPersonName { get; set; }

        [StringLength(12, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = 10)]
        [Display(Name = "PHONE_NUMBER")]
        public string PhoneNumber { get; set; }
    }
}