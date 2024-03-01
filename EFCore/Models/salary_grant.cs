using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HREF.Model
{
	public class salary_grant
	{
		[Key]
		public short sgr_id { get; set; }
		public string? salary_grant_id { get; set;}//薪酬发放编号
		public string? first_kind_id { get; set; }
		public string? first_kind_name { get; set; }
		public string? second_kind_id { get; set; }
		public string? second_kind_name { get; set; }
		public string? third_kind_id { get; set; }
		public string? third_kind_name { get; set; }
		public short human_amount { get; set; } //总人数
		public decimal? salary_standard_sum { get; set; } //标准薪酬总金额
		public decimal? salary_paid_sum { get; set; } //实发总金额

		[DisplayName("档案登记人")]
		public string? register { get; set; }
		[DisplayName("档案登记时间")]
		public DateTime? regist_time { get; set; }


		[DisplayName("档案复核人")]
		public string? checker { get; set; }

		[DisplayName("档案复核时间")]
		public DateTime? check_time { get; set; }
		[DisplayName("复核状态")]
		public short? check_status { get; set; }

	}
}
