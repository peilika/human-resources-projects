using Dapper;
using EFCore;
using EFCore.Models;
using HRIServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRServices
{
	public class HFDsService : IHFDsService
	{
		private readonly MyDbContext db;
		public HFDsService(MyDbContext db)
		{
			this.db = db;
		}

		public async Task<bool> DeleteHFsByHumanId(string humanId)
		{
			List<HFs> hfs = await db.HFs.Where(e => e.HumanId == humanId).ToListAsync();
			if (hfs.Count>0)
			{
				db.RemoveRange(hfs);
			}
			return (await db.SaveChangesAsync())>0;
			
		}

		public async Task<HFDs> GetByHumanIdAsync(string humanId)
		{
			var a = (await db.HFDs.Where(e => e.HumanId == humanId).ToListAsync())[0];
			return a;
		}

		public async Task<HFs> GetByHumanIdOnHFsAsync(string id)
		{
			var a = (await db.HFs.Where(e => e.HumanId == id).ToListAsync())[0];
			return a;
		}

		public async Task<List<HFDs>> GetByRegistTimeAndHFDs(HFDs hFDs, DateTime? dateStar, DateTime? dateEnd)
		{
			List<HFDs> list = await db.HFDs.ToListAsync();
			if (hFDs.FirstKindId!=null&&hFDs.SecondKindId!=null&&hFDs.ThirdKindId!=null)
			{
				list = list.Where(e=>e.FirstKindId == hFDs.FirstKindId&&
				e.SecondKindId == hFDs.SecondKindId&&e.ThirdKindId == hFDs.ThirdKindId).ToList();
			}
			
			if (dateStar!=null)
			{
				list =  list.Where(e => e.RegistTime >= dateStar).ToList();
				// e.RegistTime>=dateStar&&
			}
			if (dateEnd!=null)
			{
				list =  list.Where(e=>e.RegistTime <= dateEnd).ToList();
			}
			return list;
		}

		public async Task<List<HFDs>> GetByWhere(string where)
		{
			List<HFDs> list = new List<HFDs>();
			try
			{
				using (var sqlConnection = db.Database.GetDbConnection())
				{
					//调用分页的存储过程
					//@pageSize, @keyName, @tableName, @currentPage, @rows
					var parameters = new DynamicParameters();
					parameters.Add("pageSize", dbType: DbType.Int32, direction: ParameterDirection.Input, value: 8);//输入参
					parameters.Add("keyName", dbType: DbType.String, direction: ParameterDirection.Input, value: "HfdId");//输入参
					parameters.Add("where", dbType: DbType.String, direction: ParameterDirection.Input, value: where);//输入参
					parameters.Add("lie", dbType: DbType.String, direction: ParameterDirection.Input, value: "*");//输入参
					parameters.Add("tableName", dbType: DbType.String, direction: ParameterDirection.Input, value: "HFDs");//输入参
					parameters.Add("currentPage", dbType: DbType.Int32, direction: ParameterDirection.Input, value: 1);//输入参
					parameters.Add("rows", dbType: DbType.Int32, direction: ParameterDirection.Output);//输出参
					string sql = "FYByWhere";
					IEnumerable<HFDs> values = await sqlConnection.QueryAsync<HFDs>(sql, parameters, commandType: CommandType.StoredProcedure);
					if (values != null)
					{
						list = values.ToList();
					}
				}
				return list;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
		}

		public async Task<List<HFs>> GetHFsAllData()
		{
			return await db.HFs.ToListAsync();
		}

		public async Task<bool> IsUpdateHFDsTrue(HFDs hFDs)
		{
			HFDs a = await db.HFDs.FirstOrDefaultAsync(e => e.HumanId == hFDs.HumanId);
			if (a != null)
			{
				a.FirstKindId = hFDs.FirstKindId;
				a.FirstKindName = hFDs.FirstKindName;
				a.SecondKindId = hFDs.SecondKindId;
				a.SecondKindName = hFDs.SecondKindName;
				a.ThirdKindId = hFDs.ThirdKindId;
				a.ThirdKindName = hFDs.ThirdKindName;
				a.HumanMajorKindId = hFDs.HumanMajorKindId;
				a.HumanMajorKindName = hFDs.HumanMajorKindName;
				a.HumanMajorId = hFDs.HumanMajorId;
				a.HunmaMajorName = hFDs.HunmaMajorName;
				a.SalaryStandardId = hFDs.SalaryStandardId;
				a.SalaryStandardName = hFDs.SalaryStandardName;
				a.SalarySum = hFDs.SalarySum;
				a.MajorChangeAmount = hFDs.MajorChangeAmount ;
				a.Remark = hFDs.Remark;
				a.CheckStatus = hFDs.CheckStatus;
				a.Checker = hFDs.Checker;
				a.CheckTime = hFDs.CheckTime;
				return (await db.SaveChangesAsync()) > 0;
			}
			else
			{
				return false;
			}
		}

		public async Task<bool> UpdateHFDs(HFDs hFDs)
		{
			HFDs a = await db.HFDs.FirstOrDefaultAsync(e => e.HumanId == hFDs.HumanId);
			if (a != null)
			{
				HFs hfs = new HFs()
				{
					HumanId = a.HumanId,
					HumanName = a.HumanName,
					HumanAddress = a.HumanAddress,
					HumanPostcode = a.HumanPostcode,
					HumanProDesignation = a.HumanProDesignation,
					HumanTelephone = a.HumanTelephone,
					HumanMobilephone = a.HumanMobilephone,
					HumanBank = a.HumanBank,
					HumanAccount = a.HumanAccount,
					HumanQQ = a.HumanQQ,
					HumanEmail = a.HumanEmail,
					HumanHobby = a.HumanHobby,
					HumanSpeciality	= a.HumanSpeciality,
					HumanSex = a.HumanSex,
					HumanReligion = a.HumanReligion,
					HumanParty = a.HumanParty,
					HumanNationality	= a.HumanNationality,
					HumanRace = a.HumanRace,
					HumanBirthday = a.HumanBirthday,
					HumanBirthplace = a.HumanBirthplace,
					HumanAge = a.HumanAge,
					HumanEducatedDegree = a.HumanEducatedDegree,
					HumanEducatedYears = a.HumanEducatedYears,
					HumanEducatedMajor = a.HumanEducatedMajor,
					HumanSocietySecurityId = a.HumanSocietySecurityId,
					HumanIdCard = a.HumanIdCard,
					CheckStatus = a.CheckStatus,
					Register = a.Register,
					RegistTime = a.RegistTime,
					FirstKindId = hFDs.FirstKindId,
					FirstKindName = hFDs.FirstKindName,
					SecondKindId = hFDs.SecondKindId,
					SecondKindName = hFDs.SecondKindName,
					ThirdKindId = hFDs.ThirdKindId,
					ThirdKindName = hFDs.ThirdKindName,
					HumanMajorKindId = hFDs.HumanMajorKindId,
					HumanMajorKindName = hFDs.HumanMajorKindName,
					HumanMajorId = hFDs.HumanMajorId,
					HunmaMajorName = hFDs.HunmaMajorName,
					SalaryStandardId = hFDs.SalaryStandardId,
					SalaryStandardName = hFDs.SalaryStandardName,
					SalarySum = hFDs.SalarySum,
					MajorChangeAmount = hFDs.MajorChangeAmount,
					Remark = hFDs.Remark,
				};
				await db.HFs.AddAsync(hfs);
				return (await db.SaveChangesAsync()) >0;
			}
			else
			{
				return false;
			}
		}
	}
}
