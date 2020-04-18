using System.Linq;

namespace GravityDAL.Helpers
{
    static class QueryableExtensions
    {
        public static IQueryable<T> Page<T>(this IOrderedQueryable<T> query, int page = 1, int pageSize = 20)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
