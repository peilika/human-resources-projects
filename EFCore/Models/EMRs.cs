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
	/// 职位发表登记表
	/// </summary>
	public class EMRs
	{
		[Key]
		public int MreId { get; set; }//编号
		public string FirstKindId { get; set; }//一级机构编号
		public string FirstKindName { get; set; }//一级机构名称
		public string SecondKindId { get; set; }//二级机构编号
		public string SecondKindName { get; set; }//二级机构名称
		public string ThirdKindId { get;set; }//三级机构编号
		public string ThirdKindName { get;set; }//三级机构名称
		public string MajorId { get; set; }//职位编号
		public string MajorName { get; set; }//职位名称
		public string HumanAmount { get; set; }//招聘人数
		public string EngageType { get; set; }//招聘类型
		public DateTime Deadline { get; set; }//截至日期
		public string Register { get; set; }//登记人
		public string Changer { get; set; }//变更人
		public DateTime RegistTime { get; set; }//登记时间
		public DateTime ChangeTime { get; set; }//变更时间
		public string MajorDescribe { get; set; }//职位描述
		public string EngageRequired { get; set; }//招聘要求

	}
}
