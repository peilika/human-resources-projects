using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
	public interface IHFDsService
	{
		/// <summary>
		/// 根据传入的对象和两个登记时间查询对象的集合
		/// </summary>
		/// <param name="hFDs"></param>
		/// <param name="dateStar"></param>
		/// <param name="dateEnd"></param>
		/// <returns></returns>
		Task<List<HFDs>> GetByRegistTimeAndHFDs(HFDs hFDs, DateTime? dateStar, DateTime? dateEnd);
		/// <summary>
		/// 根据传入的档案编号查询对象返回
		/// </summary>
		/// <param name="humanId"></param>
		/// <returns></returns>
		Task<HFDs> GetByHumanIdAsync(string humanId);
		/// <summary>
		/// 根据传入的对象进行修改
		/// </summary>
		/// <param name="hFDs"></param>
		/// <returns></returns>
		Task<bool> UpdateHFDs(HFDs hFDs);
		/// <summary>
		/// 获取所有更改待审核的数据
		/// </summary>
		/// <returns></returns>
		Task<List<HFs>> GetHFsAllData();
		/// <summary>
		/// 根据传入的humanid获取数据所做的更改
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<HFs> GetByHumanIdOnHFsAsync(string id);
		/// <summary>
		/// 根据传入的对象进行修改
		/// </summary>
		/// <param name="hfds"></param>
		/// <returns></returns>
		Task<bool> IsUpdateHFDsTrue(HFDs hfds);
		/// <summary>
		/// 根据传入的编号删除更改记录
		/// </summary>
		Task<bool> DeleteHFsByHumanId(string humanId);
		/// <summary>
		/// 根据传入的条件进行查询
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		Task<List<HFDs>> GetByWhere(string sql);
	}
}
