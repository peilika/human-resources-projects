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
	public class CMsService : ICMsService
	{
		private readonly MyDbContext db;
		public CMsService(MyDbContext db)
		{
			this.db = db;
		}
		/// <summary>
		/// 获取所有的职位
		/// </summary>
		/// <returns></returns>
		public async Task<List<CMs>> GetAllAsync()
		{
			return await db.CMs.ToListAsync();
		}
		public async Task<List<CMs>> GetAsync(string majorKindId, string majorId)
		{
			List<CMs> cms = await db.CMs
				.Where(c => c.MajorKindId == majorKindId && c.MajorId == majorId)
				.ToListAsync();

			return cms;
		}

		public async Task<CMs> GetByMajorId(string majorId, string majorKinfId)
		{
			try
			{
				return (await db.CMs.Where(e => e.MajorId == majorId && e.MajorKindId == majorKinfId).ToListAsync())[0];
			}catch	(Exception ex)
			{
				throw new Exception();
			}
		}
	}
}
