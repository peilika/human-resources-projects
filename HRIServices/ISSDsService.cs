using EFCore.Models;
using HREF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HRIServices
{
	/// <summary>
	/// 薪酬发放单详细信息表操作
	/// </summary>
	public interface ISSDsService
	{
		Task<List<SSDs>> GetByStandardIdAsync(string standardId);
		Task<List<SG>> queryDJ();
		Task<HFs> queryid(string SalaryStandardId);
		//根据id查询人力资源表的数据
		Task<HFs> HuQuid(string standard_id);
		//添加
		Task<int> Add(salary_grant salary_Grant);
		Task<int> Add1(salary_grant_details salary_Grant_Details);
		//修改人力表的状态
		Task<int> Upd(string id);


		//根据salary_grant_id查询salary_grant_details这个表的数据
		Task<List<salary_grant_details>> Qugerid(string salary_grant_id);
		//根据human_id查询salary_standard_details表的数据
		Task<List<salary_standard_detail>> Qulsid(string human_id);

		Task<salary_grant> Qu1(string salary_grant_id);
		//复核修改状态
		Task<int> Fuhe(salary_grant salary_Grant);




		//查看发放详细表
		Task<List<SGD2>> querySGD2(string id);
		//查看sss表的
		Task<List<SSDs>> QUERYSSDS(string id);
		//修改薪资的
		Task<int> updateSSDS(int id, int a, int b, int c, int d,int dd2);
		Task<int> updateSG(string name, string date,string StandardId);
		Task<int> updateSG(string name, string date, string StandardId,bool k);
		//根据gid查看数据
		Task<SGD2> QUERYsdg2(int id);
		//查看
		Task<List<SG>> sG(string ID,string A,string B,string C);
		//薪资发放做验证
		Task<int> Sgyz(SG sG, HFDs hFDs);
	}
}
