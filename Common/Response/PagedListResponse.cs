using System.Collections.Generic;
using System.Linq;

namespace Common.Response
{
    /// <summary>
    /// paged list which returned by api methods
    /// </summary>
    /// <typeparam name="T">generic api paged list</typeparam>
    public class PagedListResponse<T>
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public int RecordsTotal { get; set; }
    }
}
