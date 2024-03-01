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
	public class CFFKsService : ICFFKsService
	{
		private readonly MyDbContext db;

		public CFFKsService(MyDbContext db)
		{
			this.db = db;
		}
		/// <summary>
		/// 进行添加操作
		/// </summary>
		/// <param name="cFFKs"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<bool> AddAsync(CFFKs cFFKs)
		{
			try
			{
				await db.CFFKs.AddAsync(cFFKs);
				return await db.SaveChangesAsync() > 0;
			}catch (Exception ex)
			{
				return false;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ffkId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<bool> DeleteAsync(int ffkId)
		{
			try
			{
				CFFKs cffk = await db.CFFKs.FindAsync(ffkId);
				db.CFFKs.Remove(cffk);
				return await db.SaveChangesAsync() > 0;
			}catch(Exception ex)
			{
				return false;
			}
		}

		/// <summary>
		/// 获取所有的一级机构
		/// </summary>
		/// <returns></returns>
		public Task<List<CFFKs>> GetAllAsync()
		{
			try
			{
				return db.CFFKs.ToListAsync();
			}catch (Exception ex)
			{
				return null;
			}
		}
		/// <summary>
		/// 根据传入的ffkId查询一级机构
		/// </summary>
		/// <param name="ffkId"></param>
		/// <returns></returns>
		public async Task<CFFKs> GetByFfkIdAsync(int ffkId)
		{
			return await db.CFFKs.FindAsync(ffkId);
		}
		/// <summary>
		/// 根据传入的对象进行修改
		/// </summary>
		/// <param name="cFFKs"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<bool> UpdateAsync(CFFKs cFFKs)
		{
			try
			{
				CFFKs cffk = await db.CFFKs.FindAsync(cFFKs.FfkId);
				cffk.FirstKindName = cFFKs.FirstKindName;
				cffk.FirstKindSalaryId = cFFKs.FirstKindSalaryId;
				cffk.FirstKindSaleId = cFFKs.FirstKindSaleId; 
				return await db.SaveChangesAsync() > 0;
			}catch(Exception ex)
			{
				return false;
			}
		}
		/// <summary>
		/// 根据firstKindId查询一级
		/// </summary>
		/// <param name="firstKindId"></param>
		/// <returns></returns> 
		public async Task<CFFKs> GetByFirstKindIdAsync(string firstKindId)
		{
			try
			{
				List<CFFKs> cFFKs = await db.CFFKs.ToListAsync();
				var a = cFFKs.Where(e=>e.FirstKindId == firstKindId).ToList()[0];
				return a;
			}catch (Exception ex)
			{
				return null;
			}
			
		}
	}
}
