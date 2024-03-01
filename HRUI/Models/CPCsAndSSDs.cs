namespace HRUI.Models
{
	/// <summary>
	/// 用于返回给前端包含价格属性的类
	/// </summary>
	public class CPCsAndSSDs
	{
		public int PbcId {  get; set; }
		public string AttributeKind { get; set;}
		public string AttributeName { get; set;}
		public double? Salary {  get; set;}
	}
}
