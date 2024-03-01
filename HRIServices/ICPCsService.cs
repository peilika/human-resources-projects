using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
	public interface ICPCsService
	{
		Task<List<CPCs>> Public_char();//查询属性
		Task<CPCs> Public_chars(string name);
		Task<CPCs> Public_charMZ(string name);
		Task<List<CMKs>> Public_char_cmks();//查询种类
		Task<bool> Public_char_Add(CPCs cPCs);//添加
		Task<bool> Public_char_Delete(int id);//删除
		Task<List<CPCs>> GetByAttributeKind(string sttributeKind);// 根据传入的种类查询
		Task<List<CPCs>> GetByPbcIds(int[] id); // 根据数组中的id获取对应的数据
	}
}
