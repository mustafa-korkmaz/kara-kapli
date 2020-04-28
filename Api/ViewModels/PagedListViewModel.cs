using System;
using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.ViewModels
{
    public class PagedListViewModel
    {
        /// <summary>
        /// desired start index for PagedList items
        /// </summary>
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Range(0, 5000, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "OFFSET")]
        [SnakeCaseQuery(nameof(Offset))]
        public int Offset { get; set; }

        /// <summary>
        /// desired length of PagedList items
        /// </summary>
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Range(1, 1000, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "LIMIT")]
        [SnakeCaseQuery(nameof(Limit))]
        public int Limit { get; set; }

        /// <summary>
        /// whether client needs recordsTotal or not.
        /// </summary>
        [SnakeCaseQuery(nameof(IncludeRecordsTotal))]
        public bool IncludeRecordsTotal { get; set; }

        /// <summary>
        /// search query text
        /// </summary>
        public class SearchedPagedListViewModel : PagedListViewModel
        {
            [SnakeCaseQuery(nameof(Q))]
            public string Q { get; set; }
        }

    }
}
