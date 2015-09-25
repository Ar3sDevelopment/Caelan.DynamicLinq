using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Caelan.DynamicLinq.Classes
{
	/// <summary>
	/// Describes the result of Kendo DataSource read operation. 
	/// </summary>
	[KnownType("GetKnownTypes")]
	public class DataSourceResult : DataSourceResult<object>
	{
	}

	/// <summary>
	/// Describes the result of Kendo DataSource read operation. 
	/// </summary>
	[KnownType("GetKnownTypes")]
	public class DataSourceResult<T>
	{
		/// <summary>
		/// Represents a single page of processed data.
		/// </summary>
		public IEnumerable<T> Data { get; set; }

		/// <summary>
		/// The total number of records available.
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// Represents a requested aggregates.
		/// </summary>
		public object Aggregates { get; set; }

		/// <summary>
		/// Used by the KnownType attribute which is required for WCF serialization support
		/// </summary>
		/// <returns></returns>
		protected static Type[] GetKnownTypes()
		{
			return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName.StartsWith("DynamicClasses"))?.GetTypes().Where(t => t.Name.StartsWith("DynamicClass")).ToArray() ?? new Type[0];
		}
	}
}
