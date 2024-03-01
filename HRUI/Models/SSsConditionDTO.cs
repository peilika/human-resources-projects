namespace HRUI.Models
{
	/// <summary>
	/// 接收查询 薪酬标准基本信息表 的条件
	/// </summary>
	public class SSsConditionDTO
	{
		public string? StandardId {  get; set; }
		public string? StandardName { get; set; }
		public string? DateOne { get; set; }
		public string? DateTwo { get; set; }
	}
}
