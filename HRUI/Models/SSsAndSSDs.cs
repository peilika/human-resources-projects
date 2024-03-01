namespace HRUI.Models
{

	/// <summary>
	/// 用于接收薪酬标准登记传入的数据
	/// </summary>
	public class SSsAndSSDs
    {
		public string StandardId {  get; set; }	// 薪酬标准编号
		public string StandardName { get; set; }	// 薪酬标准名称
		public double? SalarySum {  get; set; }	// 总金额
		public string? Designer {  get; set; }	// 制定人名称
		public int Register {  get; set; }	// 登记人编号
		public DateTime RegistTime {  get; set; }	// 登记时间
		public string Remark {  get; set; }	// 备注
		public string SelOptionsAllData {  get; set; }	// 选中的项目的详细信息
	}
}
