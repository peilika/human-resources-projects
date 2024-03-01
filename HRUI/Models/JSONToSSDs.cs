using Newtonsoft.Json;

namespace HRUI.Models
{
	public class JSONToSSDs
	{
		[JsonProperty("pbcId")]
		public int PbcId { get; set; }

		[JsonProperty("attributeKind")]
		public string? AttributeKind { get; set; }

		[JsonProperty("attributeName")]
		public string? AttributeName { get; set; }

		[JsonProperty("salary")]
		public string? Salary { get; set; }
	}
}
