namespace HRUI.Models
{
	/// <summary>
	/// 用于辅助前端修改页面的类
	/// </summary>
	public class HFDsDTO
	{
		public string HumanId { get; set; } // 档案编号
		public string HumanName { get; set; }   // 姓名
		public string OldKindName { get; set; } // 原来的一二三级机构名称（用/区分）
		public string OldHMKindName { get; set; }   // 原来的职位分类名称和职位名称（用/区分）
		public string OldSalaryStandardName { get; set; }   // 原来的薪酬标准名称
		public string OldSalarySum { get; set; }    // 原来的薪酬总额
		public string JsonNewKindId { get; set; }   // json新一二三级机构的编号
		public string JsonNewHMKindId {  get; set; }	// json新职位分类编号和职位编号
		public string SalaryStandardName { get; set; }	// 新的薪酬标准编号
		public string SalarySum {  get; set; }	// 新的薪酬标准总额
		public string Register {  get; set; }	// 登记人
		public DateTime RegistTime {  get; set; }	// 登记时间
	}
}
