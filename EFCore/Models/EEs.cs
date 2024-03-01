using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
	/// <summary>
	/// 试卷登记表
	/// </summary>
	public class EEs
	{
		[Key]
		public int ExaId { get; set; }//主键
		public string ExamNumber { get; set; }//试卷编号
		public int MajorKindId { get; set; }//职位分类编号
		public string MajorKindName { get; set; }//职位分类名称
		public int MajorId { get; set; }//职位编号
		public string MajorName { get; set; }//职位名称
		public string Register { get; set; }//登记人
		public DateTime RegistTime { get; set; }//登记时间
		public DateTime LimiteTime { get; set; }//答题限时

	}
}
