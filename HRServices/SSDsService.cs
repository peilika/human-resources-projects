using Dapper;
using EFCore;
using EFCore.Models;
using HREF.Model;
using HRIServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HRServices
{
	/// <summary>
	/// 薪酬标准单详细信息表操作
	/// </summary>
	public class SSDsService : ISSDsService
	{
		string L = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True;Trust Server Certificate=True";
		private readonly MyDbContext db;
		public SSDsService(MyDbContext db)
		{
			this.db = db;
		}

		/// <summary>
		/// 根据传入的薪酬标准单编号查询对应的数据
		/// </summary>
		/// <param name="standardId"></param>
		/// <returns></returns>
		public async Task<List<SSDs>> GetByStandardIdAsync(string standardId)
		{
			return await db.SSDs.Where(e=>e.StandardId == standardId).ToListAsync();
		}

		public async Task<List<SG>> queryDJ()
		{
			try
			{
				using (var connection = new SqlConnection("Data Source=.;Initial Catalog=HR_DB;Integrated Security=True;Trust Server Certificate=True"))
				{
					await connection.OpenAsync();
					var result = await connection.QueryAsync<SG>("SELECT * FROM  sGs");
					return result.ToList();
				}
			}
			catch (Exception ex)
			{
				// 处理错误，例如记录日志或返回空列表
				Console.WriteLine($"发生错误：{ex.Message}");
				return new List<SG>();
			}
		}

		public async Task<HFs> queryid(string SalaryStandardId)
		{
			return await db.HFs.FirstOrDefaultAsync(x => x.SalaryStandardId == SalaryStandardId);
		}
		public async Task<HFs> HuQuid(string standard_id)
		{
			return await db.HFs.FirstOrDefaultAsync(x => x.SalaryStandardId == standard_id);
		}
		public Task<int> Add(salary_grant salary_Grant)
		{
			db.salary_grant.Add(salary_Grant);
			return db.SaveChangesAsync();
		}

		public Task<int> Add1(salary_grant_details salary_Grant_Details)
		{
			db.salary_grant_details.Add(salary_Grant_Details);
			return db.SaveChangesAsync();
		}

		public async Task<int> Upd(string id)
		{
			var detail = await db.HFs.FirstOrDefaultAsync(s => s.SalaryStandardId == id);

			if (detail != null)
			{
				detail.HumanFileStatus = 1;

				return await db.SaveChangesAsync();
			}
			else
			{
				return 0;
			}
		}
		public async Task<salary_grant> Qu1(string salary_grant_id)
		{
			return await db.salary_grant.FirstOrDefaultAsync(x => x.salary_grant_id == salary_grant_id);
		}

		public async Task<List<salary_grant_details>> Qugerid(string salary_grant_id)
		{
			return await db.salary_grant_details.Where(x => x.salary_grant_id == salary_grant_id).ToListAsync();
		}

		public async Task<List<salary_standard_detail>> Qulsid(string human_id)
		{
			return await db.salary_standard_detail.Where(x => x.standard_id == human_id).ToListAsync();
		}


		public async Task<int> Fuhe(salary_grant salary_Grant)
		{
			var detail = await db.salary_grant.FirstOrDefaultAsync(s => s.salary_grant_id == salary_Grant.salary_grant_id);

			if (detail != null)
			{
				detail.check_status = 1;
				return await db.SaveChangesAsync();
			}
			else
			{
				return 0;
			}
		}

		public async Task<List<SGD2>> querySGD2(string id)
		{
			try
			{
				using (var connection = new SqlConnection(L))
				{
					await connection.OpenAsync();
					var result = await connection.QueryAsync<SGD2>("SELECT * FROM SGD2 WHERE SalaryGrantId = @id", new { id });
					return result.ToList();
				}
			}
			catch (Exception ex)
			{
				// 处理异常，例如记录日志或返回空列表
				Console.WriteLine($"查询SGD2数据时发生错误：{ex.Message}");
				return new List<SGD2>();
			}
		}

		public async Task<List<SSDs>> QUERYSSDS(string id)
		{
			try
			{
				return await db.SSDs.Where(x => x.StandardId == id).ToListAsync();
			}
			catch (Exception ex)
			{
				// 处理异常，例如记录日志或返回空列表
				Console.WriteLine($"查询SGD2数据时发生错误：{ex.Message}");
				return new List<SSDs>();
			}
		}

		public async Task<int> updateSSDS(int id, int a, int b, int c, int d,int dd2)
		{
			using (var connection = new SqlConnection(L))
			{
				await connection.OpenAsync();

				
				var query = "UPDATE SGD2 SET BounsSum = @A, SaleSum = @B, DeductSum = @C, SalaryStandardSum = @E , SalaryPaidSum= @D WHERE GrdId = @Id";

				// 执行 UPDATE 语句，并传递参数
				var result = await connection.ExecuteAsync(query, new { Id = id, A = a, B = b, C = c, D = d ,E=dd2});

				// 返回受影响的行数
				return result;
			}
		}

		public async Task<int> updateSG(string name, string date, string StandardId)
		{
			using (var connection = new SqlConnection(L))
			{
				await connection.OpenAsync();
				var query = "UPDATE sGs SET Register = @Name, RegistTime = @Date WHERE  SalaryGrantId = @Id";

				// 执行 UPDATE 语句，并传递参数
				var result = await connection.ExecuteAsync(query, new { Name = name, Date =date,Id=StandardId });

				// 返回受影响的行数
				return result;
			}
		}

		public async Task<SGD2> QUERYsdg2(int id)
		{
			return await db.SGD2.FirstOrDefaultAsync(x => x.GrdId == id);
		}

		public async Task<int> updateSG(string name, string date, string StandardId, bool k)
		{
			using (var connection = new SqlConnection(L))
			{
				await connection.OpenAsync();
				var query = "UPDATE sGs SET Checker = @Name, CheckTime = @Date,CheckStatus=@K WHERE  SalaryGrantId = @Id";

				// 执行 UPDATE 语句，并传递参数
				var result = await connection.ExecuteAsync(query, new { Name = name, Date = date, Id = StandardId ,K=k});

				// 返回受影响的行数
				return result;
			}
		}

		public async Task<List<SG>> sG(string ID, string A, string B, string C)
		{
			return await db.sGs.Where(x => x.SalaryGrantId == ID).ToListAsync();
		}


		public async Task<int> Sgyz(SG sG, HFDs hFDs)
		{
			int result = 0;

			try
			{
				using (var connection = new SqlConnection(L))
				{
					await connection.OpenAsync();

					string query = $"SELECT COUNT(*) FROM sGs WHERE FirstKindId='{sG.FirstKindId}' AND SecondKindId='{sG.SecondKindId}' AND ThirdKindId='{sG.ThirdKindId}'";
					SqlCommand command = new SqlCommand(query, connection);
					int count = (int)await command.ExecuteScalarAsync();

					if (count > 0)
					{
						string updateQuery = $"UPDATE sGs SET  SalaryStandardSum=SalaryStandardSum+'{sG.SalaryStandardSum}',  HumanAmount = HumanAmount + 1 WHERE FirstKindId='{sG.FirstKindId}' AND SecondKindId='{sG.SecondKindId}' AND ThirdKindId='{sG.ThirdKindId}'";
						SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
						int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

						if (rowsAffected > 0)
						{
							string insertQuery2 = $"INSERT INTO SGD2 (SalaryStandardSum,SalaryGrantId, HumanId, HumanName) VALUES ('{sG.SalaryStandardSum}','{sG.SalaryGrantId}', '{hFDs.HumanId}', '{hFDs.HumanName}')";
							SqlCommand insertCommand2 = new SqlCommand(insertQuery2, connection);
							int rowsAffected2 = await insertCommand2.ExecuteNonQueryAsync();

							if (rowsAffected2 > 0)
							{
								// 数据插入成功
								result = 1;
							}
							else
							{
								// 数据插入失败
								result = 0;
							}
						}
						else
						{
							// 数据更新失败
							result = 0;
						}
					}
					else
					{
						string insertQuery = $"INSERT INTO sGs ([SalaryStandardSum],FirstKindId, FirstKindName, SecondKindName, ThirdKindName, SalaryStandardId, SalaryGrantId, SecondKindId, ThirdKindId,  HumanAmount) VALUES (0+{sG.SalaryStandardSum},'{sG.FirstKindId}', '{sG.FirstKindName}', '{sG.SecondKindName}', '{sG.ThirdKindName}', '{sG.SalaryStandardId}', '{sG.SalaryGrantId}', '{sG.SecondKindId}', '{sG.ThirdKindId}',  1)";
						SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
						int rowsAffected = await insertCommand.ExecuteNonQueryAsync();

						if (rowsAffected > 0)
						{
							string insertQuery2 = $"INSERT INTO SGD2 (SalaryGrantId, HumanId, HumanName) VALUES ('{sG.SalaryGrantId}', '{hFDs.HumanId}', '{hFDs.HumanName}')";
							SqlCommand insertCommand2 = new SqlCommand(insertQuery2, connection);
							int rowsAffected2 = await insertCommand2.ExecuteNonQueryAsync();

							if (rowsAffected2 > 0)
							{
								// 数据插入成功
								result = 2;
							}
							else
							{
								// 数据插入失败
								result = 0;
							}
						}
						else
						{
							// 数据插入失败
							result = 0;
						}
					}
				}
			}
			catch (Exception ex)
			{
				// 异常处理
				Console.WriteLine(ex.Message);
				result = -1;
			}

			return result;
		}
	}
}
