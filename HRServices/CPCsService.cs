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
	public class CPCsService : ICPCsService
	{
		private readonly MyDbContext db;

		public CPCsService(MyDbContext db)
		{
			this.db = db;
		}
		//查询属性
		public async Task<List<CPCs>> Public_char()
		{
			return await db.CPCs.ToListAsync();
		}
		public async Task<CPCs> Public_chars(string name)
		{
			using (var context = db)
			{
				// 通过名字查询 CPCs 表中的记录
				var cpc = await context.CPCs.FirstOrDefaultAsync(c => c.AttributeName == name);
				return cpc;
			}
		}
		public async Task<CPCs> Public_charMZ(string name)
		{
			var cpc = await db.CPCs.FirstOrDefaultAsync(c => c.AttributeName == name);
			return cpc;
		}
		//查询种类
		public async Task<List<CMKs>> Public_char_cmks()
		{
			return await db.CMKs.ToListAsync();
		}
		//添加
		public async Task<bool> Public_char_Add(CPCs cPCs)
		{
			db.CPCs.Add(cPCs);
			return await db.SaveChangesAsync() > 0;
		}
		//删除
		public async Task<bool> Public_char_Delete(int id)
		{
			db.CPCs.Remove(await db.CPCs.FindAsync(id));
			return db.SaveChanges() > 0;
		}
		/// <summary>
		/// 根据属性查询
		/// </summary>
		/// <param name="sttributeKind"></param>
		/// <returns></returns>
		public async Task<List<CPCs>> GetByAttributeKind(string sttributeKind)
		{
			return await db.CPCs.Where(e=>e.AttributeKind == sttributeKind).ToListAsync();
		}
		/// <summary>
		/// 根据传入的数组中的id获取数据
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<List<CPCs>> GetByPbcIds(int[] id)
		{
			List<CPCs> list = new List<CPCs>();
			foreach (var a in id)
			{
				list.Add(await db.CPCs.FindAsync(a));
			}
			return list;
		}
	}
}
