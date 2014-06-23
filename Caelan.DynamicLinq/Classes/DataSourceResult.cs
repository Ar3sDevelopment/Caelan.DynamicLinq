using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Caelan.DynamicLinq.Classes
{
	[DataContract]
	public class DataSourceResult : DataSourceResult<object>
	{
	}

	[DataContract]
	public class DataSourceResult<T>
	{
		[DataMember]
		public IEnumerable<T> Data { get; set; }

		[DataMember]
		public int Total { get; set; }
	}
}
