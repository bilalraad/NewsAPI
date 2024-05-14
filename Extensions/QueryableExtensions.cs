using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Helpers;

namespace NewsAPI.Middlewares
{
    public static class QueryableExtensions
    {

        public static async Task<PaginatedList<T>> PaginateAsync<T>(this IQueryable<T> query, PagingDto PagingDto)
        {
            if (PagingDto.PageSize >= 1000) PagingDto.PageSize = 25;
            if (PagingDto.PageNumber <= 0) PagingDto.PageNumber = 1;
            int count = query.Count();
            List<T> data = await query.Skip((PagingDto.PageNumber - 1) * PagingDto.PageSize).Take(PagingDto.PageSize).ToListAsync();

            return new PaginatedList<T>
            {
                Count = count,
                Data = data
            };
        }

        public static PaginatedList<To> MapPaginatedList<From, To>(this IMapper _mapper, PaginatedList<From> list)
        {
            List<To> data = list.Data.Select(d => _mapper.Map<To>(d)).ToList();
            return new PaginatedList<To>
            {
                Count = list.Count,
                Data = data
            };
        }

        public static IOrderedQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string propertyName, bool isDescending = false)
        {
            var entityType = typeof(T);
            var propertyInfo = entityType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found on type '{entityType.Name}'.");
            }

            var parameter = Expression.Parameter(entityType, "x");
            var property = Expression.Property(parameter, propertyInfo);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = isDescending ? "OrderByDescending" : "OrderBy";
            var resultExpression = Expression.Call(typeof(Queryable), methodName, new[] { typeof(T), propertyInfo.PropertyType }, query.Expression, Expression.Quote(lambda));

            return (IOrderedQueryable<T>)query.Provider.CreateQuery<T>(resultExpression);
        }

    }
}