using System.Collections.Generic;

namespace GravityDAL.PageModels
{
    public class PaginatedResult<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Total { get; set; }
        public IList<T> Items { get; set; }
    }
}
