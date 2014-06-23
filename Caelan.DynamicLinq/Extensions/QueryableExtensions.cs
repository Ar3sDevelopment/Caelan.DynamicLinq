using System;
using System.Collections.Generic;
using System.Linq;
using Caelan.DynamicLinq.Classes;

namespace Caelan.DynamicLinq.Extensions
{
	public static class QueryableExtensions
	{
		public static DataSourceResult<T> ToDataSourceResult<T>(this IQueryable<T> queryable, int take, int skip, IEnumerable<Sort> sort, Filter filter)
		{
			queryable = queryable.Filter(filter);

			var total = queryable.Count();

			queryable = queryable.Sort(sort);

			if (take > 0)
			{
				queryable = queryable.Page(take, skip);
			}

			return new DataSourceResult<T>
			{
				Data = queryable.ToList(),
				Total = total
			};
		}

		private static IQueryable<T> Filter<T>(this IQueryable<T> queryable, Filter filter)
		{
			if (filter == null || filter.Logic == null) return queryable;

			var filters = filter.All();
			var values = filters.Select(f => f.Value).ToArray();
			var predicate = filter.ToExpression(filters);

			queryable = queryable.Where(predicate, values);

			return queryable;
		}

		private static IQueryable<T> Sort<T>(this IQueryable<T> queryable, IEnumerable<Sort> sort)
		{
			if (sort == null) return queryable;

			var sortList = sort.ToList();

			if (!sortList.Any()) return queryable;

			var ordering = String.Join(",", sortList.Select(s => s.ToExpression()));

			return queryable.OrderBy(ordering);
		}

		private static IQueryable<T> Page<T>(this IQueryable<T> queryable, int take, int skip)
		{
			return queryable.Skip(skip).Take(take);
		}
	}
}
