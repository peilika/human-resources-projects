namespace HRUI.Models
{
	/// <summary>
	/// 用于接收变更的值
	/// </summary>
	public class SSAndSSDDTO
	{
		public string StandardId {  get; set; }
		public string StandardName { get; set; }
		public float SalarySum { get; set; }
		public string Designer {  get; set; }
		public int Changer {  get; set; }
		public DateTime ChangeTime {  get; set; }
		public string Remark { get; set; }
		public string JsonSSDs {  get; set; }
	}
}
