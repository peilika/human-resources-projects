namespace HRUI.Models
{
	public class CMAndCMKDTO
	{
		public int Id { get; set; }//主键
		public string MajorKindId { get; set; }//职位分类编号
		public string MajorKindName { get; set; }//职位分类名称
		public List<CMAndCMKDTO> Children {  get; set; } 
	}
}
