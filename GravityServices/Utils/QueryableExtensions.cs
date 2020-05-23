using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using GravityDAL.PageModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace GravityServices.Utils
{
    static class QueryableExtensions
    {
        public async static Task<PaginatedResult<TDto>> CreatePaginatedResultAsync<TDto>(this IQueryable<ApplicationUser> query, PaginatedRequest paginatedRequest, IMapper mapper)            
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

        private static IQueryable<ApplicationUser> Paginate<ApplicationUser>(this IQueryable<ApplicationUser> query, PaginatedRequest paginatedRequest)
        {
            var entities = query.Skip((paginatedRequest.PageIndex) * paginatedRequest.PageSize).Take(paginatedRequest.PageSize);
            return entities;
        }

        private static IQueryable<ApplicationUser> Sort<ApplicationUser>(this IQueryable<ApplicationUser> query, PaginatedRequest pagedRequest)
        {
            if (!string.IsNullOrWhiteSpace(pagedRequest.ColumnNameForSorting))
            {
                query = query.OrderBy(pagedRequest.ColumnNameForSorting + " " + pagedRequest.SortDirection);
            }
            return query;
        }

        private static IQueryable<ApplicationUser> ApplyFilters<ApplicationUser>(this IQueryable<ApplicationUser> query, PaginatedRequest pagedRequest)
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

