using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
	public interface IPYsService
	{
		Task<List<PYs>> Salary_item();//查询
		Task<bool> Salary_Add(PYs item);//添加
		Task<bool> Salary_Remove(int id);//删除
	}
}
