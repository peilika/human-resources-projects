using EFCore.Models;

namespace HRUI.Models
{
	/// <summary>
	/// 一级二级关系类
	/// </summary>
	public class CFK
	{
		public int FkId {  get; set; }	// 主键
		public string KindId {  get; set; }	// 机构编号
		public string KindName {  get; set; }	// 机构名称
		public string KindSalaryId {  get; set; }	// 机构薪酬发放责任人编号
		public string KindSaleId {  get; set; }	// 机构销售责任人编号
		public List<CFK> Children { get; set; }	// 子集
	}
}
