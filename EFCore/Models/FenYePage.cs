using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
	//分页表
	public class FenYePage
	{
		public int PageSize { get; set; }//当前页大小
		public int PageIndex { get; set; }//当前页
	}
}
