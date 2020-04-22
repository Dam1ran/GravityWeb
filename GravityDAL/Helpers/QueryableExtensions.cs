using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using GravityDAL.PageModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace GravityDAL.Helpers
{
    static class QueryableExtensions
    {
        public static IQueryable<T> Page<T>(this IOrderedQueryable<T> query, int page = 1, int pageSize = 20)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public async static Task<PaginatedResult<TDto>> CreatePaginatedResultAsync<T, TDto>(this IQueryable<T> query, PaginatedRequest paginatedRequest, IMapper mapper)
            where T : BaseEntity
            where TDto : class
        {
            var projectionResult = query.ProjectTo<TDto>(mapper.ConfigurationProvider);

            projectionResult = projectionResult.ApplyFilters(paginatedRequest);

            projectionResult = projectionResult.Sort(paginatedRequest);

            var total = await projectionResult.CountAsync();

            projectionResult = projectionResult.Paginate(paginatedRequest);

            var listResult = await projectionResult.ToListAsync();

            return new PaginatedResult<TDto>()
            {
                Items = listResult,
                PageSize = paginatedRequest.PageSize,
                PageIndex = paginatedRequest.PageIndex,
                Total = total
            };
        }

        private static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginatedRequest paginatedRequest)
        {
            var entities = query.Skip((paginatedRequest.PageIndex) * paginatedRequest.PageSize).Take(paginatedRequest.PageSize);
            return entities;
        }

        private static IQueryable<T> Sort<T>(this IQueryable<T> query, PaginatedRequest pagedRequest)
        {
            if (!string.IsNullOrWhiteSpace(pagedRequest.ColumnNameForSorting))
            {
                query = query.OrderBy(pagedRequest.ColumnNameForSorting + " " + pagedRequest.SortDirection);
            }
            return query;
        }

        private static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, PaginatedRequest pagedRequest)
        {
            var predicate = new StringBuilder();
            var requestFilters = pagedRequest.RequestFilters;
            for (int i = 0; i < requestFilters.Filters.Count; i++)
            {
                if (i > 0)
                {
                    predicate.Append($" {requestFilters.LogicalOperator} ");
                }
                predicate.Append(requestFilters.Filters[i].Path + $".{nameof(string.Contains)}(@{i})");
            }

            if (requestFilters.Filters.Any())
            {
                var propertyValues = requestFilters.Filters.Select(filter => filter.Value).ToArray();

                query = query.Where(predicate.ToString(), propertyValues);
            }

            return query;
        }

    }
}
