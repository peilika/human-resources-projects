using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HREF.Model
{
	public class salary_grant_details
	{
		[Key]
		public short grd_id { get; set; }
		public string? salary_grant_id { get; set; }//薪酬发放编号

		[DisplayName("档案编号")]
		public string? human_id { get; set; }

		[DisplayName("姓名")]
		public string? human_name { get; set; }
		public decimal? bouns_sum { get; set; } //奖励金额
		public decimal? sale_sum { get; set; } //销售绩效金额
		public decimal? deduct_sum { get; set; } //应扣金额
		public decimal? salary_standard_sum { get; set; } //标准薪酬总额
		public decimal? salary_paid_sum { get; set; } //实发薪酬总额

	}
}
