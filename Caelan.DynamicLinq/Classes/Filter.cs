using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Caelan.DynamicLinq.Classes
{
	[DataContract]
	public class Filter
	{
		[DataMember]
		public string Field { get; set; }

		[DataMember]
		public string Operator { get; set; }

		[DataMember]
		public object Value { get; set; }

		[DataMember]
		public string Logic { get; set; }

		[DataMember]
		public IEnumerable<Filter> Filters { get; set; }

		private static readonly IDictionary<string, string> Operators = new Dictionary<string, string>
        {
            {"eq", "="},
            {"neq", "!="},
            {"lt", "<"},
            {"lte", "<="},
            {"gt", ">"},
            {"gte", ">="},
            {"startswith", "StartsWith"},
            {"endswith", "EndsWith"},
            {"contains", "Contains"},
            {"doesnotcontain", "Contains"}
        };

		public IList<Filter> All()
		{
			var filters = new List<Filter>();

			Collect(filters);

			return filters;
		}

		private void Collect(ICollection<Filter> filters)
		{
			if (Filters != null && Filters.Any())
			{
				foreach (var filter in Filters)
				{
					filters.Add(filter);

					filter.Collect(filters);
				}
			}
			else
			{
				filters.Add(this);
			}
		}

		public string ToExpression(IList<Filter> filters)
		{
			if (Filters != null && Filters.Any())
			{
				return "(" + String.Join(" " + Logic + " ", Filters.Select(filter => filter.ToExpression(filters)).ToArray()) + ")";
			}

			var index = filters.IndexOf(this);
			var comparison = Operators[Operator];

			switch (Operator)
			{
				case "doesnotcontain":
					return String.Format("!{0}.{1}(@{2})", Field, comparison, index);
				case "startswith":
				case "endswith":
				case "contains":
					return String.Format("{0}.{1}(@{2})", Field, comparison, index);
				default:
					return String.Format("{0} {1} @{2}", Field, comparison, index);
			}
		}
	}
}
