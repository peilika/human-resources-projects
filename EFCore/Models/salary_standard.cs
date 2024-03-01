using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HREF.Model
{
	public class salary_standard
	{
		[Key]
		public short ssd_id { get; set; }


		[DisplayName("薪酬标准单编号")]
		public string? standard_id { get; set; }


		[DisplayName("薪酬标准单名称")]
		public string? standard_name { get; set; }

		[DisplayName("制定者名字")]
		public string? designer { get; set; }

		[DisplayName("登记人")]
		public string? register { get; set; }

		[DisplayName("复核人")]
		public string? checker { get; set; }

		[DisplayName("变更人")]
		public string? changer { get; set; }

		[DisplayName("登记时间")]
		public string? regist_time { get; set; }

		[DisplayName("复核时间")]
		public string? check_time { get; set; }

		[DisplayName("变更时间")]
		public string? change_time { get; set; }

		[DisplayName("薪酬总额")]
		public decimal? salary_sum { get; set; }

		[DisplayName("是否经过复核")]
		public short check_status { get; set; }

		[DisplayName("更改状态")]
		public short? change_status { get; set; }

		[DisplayName("复核意见")]
		public string? check_comment { get; set; }

		[DisplayName("备注")]
		public string? remark { get; set; }
	}
}
