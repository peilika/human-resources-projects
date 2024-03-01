namespace HRUI.Models
{
	/// <summary>
	/// 职位分类和职位设置
	/// </summary>
	public class CMK
	{
		public int MfkId { get; set; }//主键
		public string MajorKindId { get; set; }//职位分类编号
		public string MajorKindName { get; set; }//职位分类名称
		public List<CMK> Children { get; set; } // 子集
	}
}
