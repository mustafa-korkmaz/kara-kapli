namespace Common.Request
{
    /// <summary>
    /// paged list which returned by application layer methods 
    /// </summary>
    public class PagedListRequest
    {
        /// <summary>
        /// desired start index for PagedList items
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// desired length of PagedList items
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// whether client needs recordsTotal or not.
        /// </summary>
        public bool IncludeRecordsTotal { get; set; }
    }

    /// <summary>
    /// filtered paged list object which will be used by application methods
    /// </summary>
    public class FilteredPagedListRequest<TCriteria> : PagedListRequest
    {
        public TCriteria FilterCriteria { get; set; }
    }
}