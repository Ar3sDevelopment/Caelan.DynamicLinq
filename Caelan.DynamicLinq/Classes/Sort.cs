using System.Runtime.Serialization;

namespace Caelan.DynamicLinq.Classes
{
	[DataContract]
	public class Sort
	{
		[DataMember]
		public string Field { get; set; }

		[DataMember]
		public string Dir { get; set; }

		public string ToExpression()
		{
			return Field + " " + Dir;
		}
	}
}
