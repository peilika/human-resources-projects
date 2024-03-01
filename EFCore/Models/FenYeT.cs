using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
	public class FenYeT<T>
	{
		public int PageIndex {  get; set; }
		public int PageSize {  get; set; }
		public int DataCount { get; set; }
		public List<T> List { get; set; }
	}
}
