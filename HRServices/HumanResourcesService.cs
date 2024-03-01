using Azure;
using Dapper;
using EFCore;
using EFCore.Models;
using HRIServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace HRServices
{
	public class HumanResourcesService:IHumanResourcesService
	{
		private readonly MyDbContext db;

		public HumanResourcesService(MyDbContext db)
		{
			this.db = db;
		}
		public async Task<List<ERs>> Lists_ER(FenYePage page)
		{
			int pageSize = page.PageSize;
			int pageIndex = page.PageIndex;

			var ers = await db.ERs
				.Skip((pageIndex - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return ers;
		}
        public async Task<ERs> List_ER(int id)
		{
			return await db.ERs.FindAsync(id);
		}
		//添加
		public async Task<bool> Human_Register(HFDs hf)
		{
			db.HFDs.Add(hf);
			return await db.SaveChangesAsync()>0;
		}
		//查询
		public async Task<List<HFDs>> Check_list(FenYePage page)
		{
			int pageSize = page.PageSize;
			int pageIndex = page.PageIndex;

			var hfds = await db.HFDs
				.Where(h => h.HumanFileStatus != 2)
				.Skip((pageIndex - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return hfds;
		}
		public async Task<bool> HFDsInsert(HFDs hf)
		{
			db.HFDs.Add(hf);
			return await db.SaveChangesAsync() > 0;
		}
		public async Task<List<CMs>> SelectCMs()
		{
			return await db.CMs.ToListAsync();
		}

		//查询总数据
		public async Task<int> Check_ListCount()
		{
			return await db.HFDs.CountAsync();
		}
		//查询
		public async Task<List<CMKs>> Select()
		{
			return await db.CMKs.ToListAsync();
		}
		public async Task<List<CMs>> Select2()
		{
			return await db.CMs.ToListAsync();
		}

		//人力资源复核查询
		public async Task<HFDs> QueryLocate(int id)
		{
			return await db.HFDs.FindAsync(id);
		}		
		//复核
		public async Task<bool> Human_Update(int id, int id2, string name, DateTime time)
		{
			HFDs hf = await db.HFDs.FindAsync(id);
			hf.CheckStatus = id2;
			hf.Checker = name;
			hf.CheckTime = time;
			return db.SaveChanges() > 0;
		}

		public async Task<List<HFDs>> SelectQuery(FenYeT<HFDs> page, string selectStr)
		{
			List<HFDs> list = new List<HFDs>();
			using (var sqlConnection = db.Database.GetDbConnection())
			{
				//调用分页的存储过程
				//@pageSize, @keyName, @tableName, @currentPage, @rows
				var parameters = new DynamicParameters();
				parameters.Add("pageSize", dbType: DbType.Int32, direction: ParameterDirection.Input, value: page.PageSize);//输入参
				parameters.Add("keyName", dbType: DbType.String, direction: ParameterDirection.Input, value: "HfdId");//输入参
				parameters.Add("where", dbType: DbType.String, direction: ParameterDirection.Input, value: selectStr);//输入参
				parameters.Add("lie", dbType: DbType.String, direction: ParameterDirection.Input, value: "*");//输入参
				parameters.Add("tableName", dbType: DbType.String, direction: ParameterDirection.Input, value: "HFds");//输入参
				parameters.Add("currentPage", dbType: DbType.Int32, direction: ParameterDirection.Input, value: page.PageIndex);//输入参
				parameters.Add("rows", dbType: DbType.Int32, direction: ParameterDirection.Output);//输出参
				string sql = "FYByWhere";
				IEnumerable<HFDs> values = sqlConnection.Query<HFDs>(sql, parameters, commandType: CommandType.StoredProcedure);
				int rows = parameters.Get<int>("@rows");
				if (values != null)
				{
					list = values.ToList();
				}
			}
			return list;
		}

		public async Task<bool> HFDsUpdate(HFDs hf)
		{
			HFDs hfd = await db.HFDs.FindAsync(hf.HfdId);
			hfd.HumanProDesignation = hf.HumanProDesignation;
			hfd.HumanName = hf.HumanName;
			hfd.HumanSex = hf.HumanSex;
			hfd.HumanEmail = hf.HumanEmail;
			hfd.HumanTelephone = hf.HumanTelephone;
			hfd.HumanQQ = hf.HumanQQ;
			hfd.HumanMobilephone = hf.HumanMobilephone;
			hfd.HumanAddress = hf.HumanAddress;
			hfd.HumanPostcode = hf.HumanPostcode;
			hfd.HumanNationality = hf.HumanNationality;
			hfd.HumanBirthday = hf.HumanBirthday;
			hfd.HumanBirthplace = hf.HumanBirthplace;
			hfd.HumanRace = hf.HumanRace;
			hfd.HumanReligion = hf.HumanReligion;
			hfd.HumanParty = hf.HumanParty;
			hfd.HumanIdCard = hf.HumanIdCard;
			hfd.HumanSocietySecurityId = hf.HumanSocietySecurityId;
			hfd.HumanAge = hf.HumanAge;
			hfd.HumanEducatedDegree = hf.HumanEducatedDegree;
			hfd.HumanEducatedYears = hf.HumanEducatedYears;
			hfd.HumanEducatedMajor = hf.HumanEducatedMajor;
			hfd.SalaryStandardId = hf.SalaryStandardId;
			hfd.SalaryStandardName = hf.SalaryStandardName;
			hfd.HumanBank = hf.HumanBank;
			hfd.HumanAccount = hf.HumanAccount;
			hfd.HumanSpeciality = hf.HumanSpeciality;
			hfd.HumanHobby = hf.HumanHobby;
			hfd.HumanHistroyRecords = hf.HumanHistroyRecords;
			hfd.HumanFamilyMembership = hf.HumanFamilyMembership;
			hfd.Remark = hf.Remark;
			hfd.HumanPicture = hf.HumanPicture;
			hfd.ChangeTime = hf.ChangeTime;
			hfd.Changer = hf.Changer;
			return db.SaveChanges() > 0;
		}

		public async Task<List<HFDs>> Delete_list(FenYePage page)
		{
			int pageSize = page.PageSize;
			int pageIndex = page.PageIndex;

			var hfds = await db.HFDs
				.Where(h => h.CheckStatus == 1 )
				.Skip((pageIndex - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return hfds;
		}
		public async Task<int> Delete_listCount()
		{
			int count = 0;

			// 查询条件：CheckStatus为1且HumanFileStatus不等于2
			var query = from item in db.HFDs
						where item.CheckStatus == 1
						select item;

			count = await query.CountAsync();

			return count;
		}
		public async Task<bool> Deletion(int id)
		{
			var hfds = await db.HFDs.FindAsync(id);
			if (hfds != null)
			{
				hfds.HumanFileStatus = 1;
				await db.SaveChangesAsync();
				return true;
			}
			return false;
		}
		public async Task<bool> Recover(int id)
		{
			var hfds = await db.HFDs.FindAsync(id);
			if (hfds != null)
			{
				hfds.HumanFileStatus = 0;
				await db.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<List<HFDs>> Recovery_list(FenYePage page)
		{
			int pageSize = page.PageSize;
			int pageIndex = page.PageIndex;

			var hfds = await db.HFDs
				.Where(h => h.HumanFileStatus == 1)
				.Skip((pageIndex - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return hfds;
		}


		public async Task<List<HFDs>> Delete_forever_list(FenYePage page)
		{
			int pageSize = page.PageSize;
			int pageIndex = page.PageIndex;

			var hfds = await db.HFDs
				.Where(h => h.HumanFileStatus == 1)
				.Skip((pageIndex - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return hfds;
		}
		public async Task<bool> ForeverDelete(int id)
		{
			var hfds = await db.HFDs.FindAsync(id);
			if (hfds != null)
			{
				db.HFDs.Remove(hfds);
				await db.SaveChangesAsync();
				return true;
			}
			return false;
		}
	}
}
