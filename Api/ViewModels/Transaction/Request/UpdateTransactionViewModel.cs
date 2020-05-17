using System;
using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels.Transaction.Request
{
    public class UpdateTransactionViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "TYPE_ID")]
        [SnakeCaseQuery(nameof(TypeId))]
        public int? TypeId { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "AMOUNT")]
        [SnakeCaseQuery(nameof(Amount))]
        public double? Amount { get; set; }

        [StringLength(250, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = AppConstant.MinimumLengthForSearch)]
        [Display(Name = "AUTHORIZED_PERSON_NAME")]
        public string Description { get; set; }

        //[Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        //[Display(Name = "IS_RECEIVABLE")]
        //[SnakeCaseQuery(nameof(IsReceivable))]
        //public bool? IsReceivable { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [RegularExpression(@"^\s*(3[01]|[12][0-9]|0?[1-9])\.(1[012]|0?[1-9])\.((?:19|20)\d{2})\s*$", ErrorMessage = ValidationErrorCode.DateNotValid)]
        [Display(Name = "DATE_TEXT")]
        public string DateText { get; set; }

        public DateTime Date => DateText.ToDate();
    }
}