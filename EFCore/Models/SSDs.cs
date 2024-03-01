using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
	/// <summary>
	/// 薪酬标准单详细信息表
	/// </summary>
	public class SSDs
    {
        [Key]
        public int SdtId { get; set; }  // 主键
        public string StandardId { get; set; } // 薪酬标准单编号
        public string? StandardName { get; set; } // 薪酬标准单名称
        public int ItemId { get; set; } // 薪酬项目序号
        public string? ItemName { get; set; }    // 薪酬项目名称
        public double Salary { get; set; }  // 薪酬金额

		
	}
}
