using EFCore;
using EFCore.Models;
using HRIServices;
using Microsoft.EntityFrameworkCore;

namespace HRServices
{
	public class ClientsService:IClientsService
	{
		private readonly MyDbContext db;

		public ClientsService(MyDbContext db)
		{
			this.db = db;
		}
		public async Task<List<CMKs>> MajorSelect()
		{
			return await db.CMKs.ToListAsync();
		}
		public async Task<bool> MajorDelete(int id)
		{
			db.CMKs.Remove(await db.CMKs.FindAsync(id));
			return db.SaveChanges() > 0;
		}
		public async Task<bool> MajorInsert(CMKs cM, int majorID)
		{
			if(majorID == 0)
			{
				cM.MajorKindId = "00" + 1;
			}
			else if(majorID < 10)
			{
				cM.MajorKindId = "00" + (majorID + 1);
			}
			else if(majorID>10&& majorID<100)
			{
				cM.MajorKindId = "0" + (majorID + 1);
			}
			else
			{
				cM.MajorKindId = (majorID + 1) +"";
			}
			db.CMKs.Add(cM);
			return await db.SaveChangesAsync() > 0;
		}
		public async Task<List<int>> SelectCount()
		{
			List<CMKs> cm = await db.CMKs.ToListAsync();

			List<int> sums = new List<int>();
			foreach (var item in cm)
			{
				if (int.TryParse(item.MajorKindId, out int result))
				{
					sums.Add(result);
				}
			}
			return sums;
		}
		public async Task<List<int>> SelectCount2()
		{
			List<CMs> cm = await db.CMs.ToListAsync();

			List<int> sums = new List<int>();
			foreach (var item in cm)
			{
				if (int.TryParse(item.MajorId, out int result))
				{
					sums.Add(result);
				}
			}
			return sums;
		}


		//职位设置表
		//查询
		public async Task<List<CMs>> MajorS()
		{
			return await db.CMs.ToListAsync();
		}
		//删除
		public async Task<bool> MajorD(int id)
		{
			db.CMs.Remove(await db.CMs.FindAsync(id));
			return db.SaveChanges()>0;
		}
		//添加
		public async Task<bool> MajorA(CMs cm, int majorID)
		{
			if (majorID == 0)
			{
				cm.MajorId = "00" + 1;
			}
			else if (majorID < 10)
			{
				cm.MajorId = "00" + (majorID + 1);
			}
			else if (majorID > 10 && majorID < 100)
			{
				cm.MajorId = "0" + (majorID + 1);
			}
			else
			{
				cm.MajorId = (majorID + 1) + "";
			}
			db.CMs.Add(cm);
			return await db.SaveChangesAsync() > 0;
		}
	}
}
