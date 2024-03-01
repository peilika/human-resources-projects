using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
	public interface IClientsService
	{
		//职位分类
		Task<List<CMKs>> MajorSelect();//查询职位分类
		Task<bool> MajorDelete(int id);//删除职位分类
		Task<bool> MajorInsert(CMKs cM,int majorID);//添加职位分类
		Task<List<int>> SelectCount();//查询当前最大的数据
		Task<List<int>> SelectCount2();//查询当前最大的数据

		//职位设置表
		Task<List<CMs>> MajorS();//查询职位设置表
		Task<bool> MajorD(int id);//删除职位分类
		Task<bool> MajorA(CMs cM, int majorID);//添加职位分类
	}
}
