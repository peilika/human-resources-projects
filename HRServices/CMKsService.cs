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
	public class CMKsService : ICMKsService
	{
		private MyDbContext db;
		public CMKsService(MyDbContext db)
		{
			this.db = db;
		}
		/// <summary>
		/// 获取所有的职位分类
		/// </summary>
		/// <returns></returns>
		public async Task<List<CMKs>> GetAllAsync()
		{
			 return await db.CMKs.ToListAsync();
		}

		public async Task<CMKs> GetByMajorKindId(string majorKindId)
		{
			try
			{
				return (await db.CMKs.Where(e => e.MajorKindId == majorKindId).ToListAsync())[0];
			}catch (Exception ex)
			{
				throw new Exception();
			}
		}
	}
}
