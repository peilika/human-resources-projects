namespace HRUI.Models
{
	/// <summary>
	/// 接收 ‘薪酬标准登记复核’ 传入的数据
	/// </summary>
	public class SalarystandardCheckDTO
	{
		public string StandardId {  get; set; }	// 薪酬编号
		public string StandardName { get; set; }	// 薪酬标准名称
		public float SalarySum { get; set; }	// 薪酬总额
		public string Designer {  get; set; }	// 制定人名称
		public int Checker {  get; set; }	// 复核人编号
		public DateTime CheckTime {  get; set; }	// 复核时间
		public string CheckComment {  get; set; }	// 复核意见
		public string JsonSSDs {  get; set; }	// 详细信息
	}
}
