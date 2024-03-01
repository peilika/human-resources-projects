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
	public class CFSKsService : ICFSKsService
	{
		private readonly MyDbContext db;
		public CFSKsService(MyDbContext db)
		{
			this.db = db;
		}
		/// <summary>
		/// 根据传入的二级标签进行添加
		/// </summary>
		/// <param name="cFSKs"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<bool> AddAsync(CFSKs cFSKs)
		{
			await db.CFSKs.AddAsync(cFSKs);
			return await db.SaveChangesAsync()>0;
		}
		/// <summary>
		/// 根据传入的编号进行删除
		/// </summary>
		/// <param name="fskId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<bool> DeleteAsync(int fskId)
		{
			try
			{
				db.CFSKs.Remove(await db.CFSKs.FindAsync(fskId));
				return await db.SaveChangesAsync()>0;
			}catch (Exception ex)
			{
				return false;
			}
			
		}

		/// <summary>
		/// 获取所有的二级机构
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<List<CFSKs>> GetAllAsync()
		{
			return await db.CFSKs.ToListAsync();
		}
		/// <summary>
		/// 根据传入的二级编号查询二级标签
		/// </summary>
		/// <param name="fskId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<CFSKs> GetByFskIdAsync(int fskId)
		{
			return await db.CFSKs.FindAsync(fskId);
		}

		/// <summary>
		/// 根据二级机构编号查询二级机构
		/// </summary>
		/// <param name="secondKindId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<CFSKs> GetBySecondKindIdAsync(string secondKindId)
		{
			try
			{
				List<CFSKs> cFSKs = await db.CFSKs.ToListAsync();
				var a = cFSKs.Where(e => e.SecondKindId == secondKindId).ToList()[0];
				return a;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		/// <summary>
		/// 根据传入的FirstKindId查询对应的所有二级
		/// </summary>
		/// <param name="ffkId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<List<CFSKs>> GetCFSKsByFFKIdAsync(string firstKindId)
		{
			var a = await db.CFSKs.ToListAsync();
			var b = a.Where(e => e.FirstKindId == firstKindId).ToList();
			return b;
		}

		/// <summary>
		/// 进行修改操作
		/// </summary>
		/// <param name="cFSKs"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(CFSKs cFSKs)
		{
			try
			{
				CFSKs cfsk = await db.CFSKs.FindAsync(cFSKs.FskId);
				cfsk.SecondSalaryId = cFSKs.SecondSalaryId;
				cfsk.SecondSaleId = cFSKs.SecondSaleId;
				return await db.SaveChangesAsync()>0;
			}catch (Exception ex) { 
				return false;
			}
			
		}
	}
}
