using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HREF.Model
{
	public class salary_standard_detail
	{
		[Key]
		public short sdt_id { get; set; }


		[DisplayName("薪酬标准单编号")]
		public string? standard_id { get; set; }


		[DisplayName("薪酬标准单名称")]
		public string? standard_name { get; set; }

		[DisplayName("薪酬项目序号")]
		public short item_id { get; set; }

		[DisplayName("薪酬项目名称")]
		public string? item_name { get; set; }

		[DisplayName("薪酬金额")]
		public decimal? salary { get; set; }
	}
}
