using System.Collections.Generic;

namespace ABSD.Common.Paging
{
    public class PagedResult<T> where T : class
    {
        public int PageCount { get; set; }
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public List<T> Items { get; set; }
    }
}