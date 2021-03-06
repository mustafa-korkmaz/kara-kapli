﻿using System;
using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels.Transaction.Request
{
    public class UpdateTransactionViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "TYPE_ID")]
        public int? TypeId { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "AMOUNT")]
        public double? Amount { get; set; }

        [StringLength(250, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = AppConstant.MinimumLengthForSearch)]
        [Display(Name = "DESCRIPTION")]
        public string Description { get; set; }

        public string AttachmentName { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [RegularExpression(@"^\s*(3[01]|[12][0-9]|0?[1-9])\.(1[012]|0?[1-9])\.((?:19|20)\d{2})\s*$", ErrorMessage = ValidationErrorCode.DateNotValid)]
        [Display(Name = "DATE_TEXT")]
        public string DateText { get; set; }

        public DateTime Date => DateText.ToDate();
    }
}