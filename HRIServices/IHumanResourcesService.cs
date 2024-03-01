using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
	public interface IHumanResourcesService
	{
		Task<List<ERs>> Lists_ER(FenYePage page);
		Task<ERs> List_ER(int id);
		Task<bool> Human_Register(HFDs hF);//添加
		Task<List<HFDs>> Check_list(FenYePage page);//查询
		Task<bool> HFDsInsert(HFDs hF);//添加


        Task<int> Check_ListCount();//查询总数据
		Task<List<CMKs>> Select();//查询1
		Task<List<CMs>> SelectCMs();//查询2

		Task<HFDs> QueryLocate(int id);//人力资源复核查询（根据Id查询）
		Task<bool> Human_Update(int id, int id2, string name, DateTime time);//复核

		Task<List<HFDs>> SelectQuery(FenYeT<HFDs> page, string selectStr);//查询已复核的数据

		Task<bool> HFDsUpdate(HFDs hf);


		Task<List<HFDs>> Delete_list(FenYePage page);//查询
		Task<int> Delete_listCount();//查询总数
		Task<bool> Deletion(int id);//标记删除
		Task<bool> Recover(int id);//恢复

		Task<List<HFDs>> Recovery_list(FenYePage page);//恢复页面


		Task<List<HFDs>> Delete_forever_list(FenYePage page);//永久删除页面
		Task<bool> ForeverDelete(int id);//永久删除

	}
}