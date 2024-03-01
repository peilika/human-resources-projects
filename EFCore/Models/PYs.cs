using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
	/// <summary>
	/// 薪酬项目名称
	/// </summary>
	public class PYs
	{
		[Key]
		public int PYID { get; set; }//主键
		public string PYName { get; set; }
	}
}
