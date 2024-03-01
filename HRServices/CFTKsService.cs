using EFCore;
using EFCore.Models;
using HRIServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRServices
{
	public class CFTKsService : ICFTKsService
    {
        private readonly MyDbContext db;
        public CFTKsService(MyDbContext db)
        {
            this.db = db;
        }
		/// <summary>
		/// 对传入的三级标签进行添加
		/// </summary>
		/// <param name="cFTKs"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<bool> AddAsync(CFTKs cFTKs)
		{
			await db.CFTKs.AddAsync(cFTKs);
			return await db.SaveChangesAsync()>0;
		}
		/// <summary>
		/// 执行删除
		/// </summary>
		/// <param name="ftkId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<bool> DeleteAsync(int ftkId)
		{
			try
			{
				db.CFTKs.Remove(await db.CFTKs.FindAsync(ftkId));
				return await db.SaveChangesAsync()>0;
			}catch (Exception ex)
			{
				return false;
			}
		}

		/// <summary>
		/// 查询所有
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<List<CFTKs>> GetAllAsync()
        {
            return await db.CFTKs.ToListAsync();
        }
		/// <summary>
		/// 根据传入的三级编号查询对应的数据
		/// </summary>
		/// <param name="ftkId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<CFTKs> GetByFtkId(int ftkId)
		{
			return await db.CFTKs.FindAsync(ftkId);
		}
		/// <summary>
		/// 根据传入的对象对指定数据进行修改
		/// </summary>
		/// <param name="cFTKs"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<bool> UpdateAsync(CFTKs cFTKs)
		{
			try
			{
				CFTKs cftk = await db.CFTKs.FindAsync(cFTKs.FtkId);
				cftk.ThirdKindSaleId = cFTKs.ThirdKindSaleId;
				cftk.ThirdKindIsRetail = cFTKs.ThirdKindIsRetail;
				return await db.SaveChangesAsync()>0;
			}
			catch (Exception ex)
			{
				return false;
			}
			
		}
		public async Task<List<CFTKs>> GetCFSKsByFFKIdAsync(string firstKindId, string secondKindId, string thridKindId)
		{
			List<CFTKs> cftk = await db.CFTKs
				.Where(c => c.FirstKindId == firstKindId && c.SecondKindId == secondKindId && c.ThirdKindId == thridKindId)
				.ToListAsync();

			return cftk;
		}

		public async Task<CFTKs> GetByThirdKindId(string ThirdKindId)
		{
			try
			{
				List<CFTKs> cFSKs = await db.CFTKs.ToListAsync();
				var a = cFSKs.Where(e => e.ThirdKindId == ThirdKindId).ToList()[0];
				return a;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
