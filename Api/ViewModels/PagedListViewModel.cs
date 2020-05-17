using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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

        [StringLength(100, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "SORT_BY")]
        [SnakeCaseQuery(nameof(SortBy))]
        public string SortBy { get; set; }

        [StringLength(4, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "SORT_TYPE")]
        [SnakeCaseQuery(nameof(SortType))]
        public string SortType { get; set; }

        public SortType GetSortType()
        {
            if (string.IsNullOrEmpty(SortType))
            {
                return Common.SortType.None;
            }

            switch (SortType.ToLower())
            {
                case "asc":
                    return Common.SortType.Ascending;
                case "desc":
                    return Common.SortType.Descending;
                default:
                    return Common.SortType.None;
            }

        }
    }
}
