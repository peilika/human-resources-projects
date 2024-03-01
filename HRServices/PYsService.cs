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
	public class PYsService:IPYsService
	{
		private readonly MyDbContext db;

		public PYsService(MyDbContext db)
		{
			this.db = db;
		}
		//查询
		public async Task<List<PYs>> Salary_item()
		{
			return await db.PYs.ToListAsync();
		}
		//添加
		public async Task<bool> Salary_Add(PYs item)
		{
			await db.PYs.AddAsync(item);
			return await db.SaveChangesAsync()>0;
		}
		public async Task<bool> Salary_Remove(int id)
		{
			db.PYs.Remove(await db.PYs.FindAsync(id));
			return db.SaveChanges() > 0;
		}
	}
}
