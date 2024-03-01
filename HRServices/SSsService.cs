using Dapper;
using EFCore.Models;
using EFCore;
using HRIServices;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace HRServices
{
    public class SSsService : ISSsService
    {
		private readonly MyDbContext db;
		public SSsService(MyDbContext db)
		{
			this.db = db;
		}
		// <summary>
		// 根据传入的数据调用数据库的存储过程进行添加
		// </summary>
		// <returns></returns>
		public async Task<bool> AddSSsAsync(SSs sSs, List<SSDs> sSDs)
		{
			using (var sqlcon = db.Database.GetDbConnection())
			{
				// 定义存储过程的参数
				var parameters = new DynamicParameters();
				parameters.Add("@StandardId", sSs.StandardId);
				parameters.Add("@StandardName", sSs.StandardName);
				parameters.Add("@Designer", sSs.Designer);
				parameters.Add("@Register", sSs.Register);
				parameters.Add("@RegistTime", sSs.RegistTime);
				parameters.Add("@SalarySum", sSs.SalarySum);
				parameters.Add("@Remark", sSs.Remark);
				parameters.Add("@CheckStatus", sSs.CheckStatus);
				parameters.Add("@ChangeStatus", sSs.ChangeStatus);

				// 构建表值参数
				var table = new DataTable();
				table.Columns.Add("ItemId", typeof(int));
				table.Columns.Add("ItemName", typeof(string));
				table.Columns.Add("Salary", typeof(double));
				// 向表中添加数据
				foreach (var ss in sSDs)
				{
					table.Rows.Add(ss.ItemId, ss.ItemName, ss.Salary);
				}

				// 将表值参数添加到 DynamicParameters 对象中
				parameters.Add("@SSDsData", table.AsTableValuedParameter("dbo.SSDs"));
				// 调用存储过程
				try
				{
					return await sqlcon.ExecuteAsync("AddSSsAndSSDs", parameters, commandType: CommandType.StoredProcedure) > 0;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					throw new Exception(ex.Message);
				}
			}
		}
		/// <summary>
		/// 获取所有
		/// </summary>
		/// <returns></returns>
		public async Task<List<SSs>> GetAll()
		{
			try
			{
				List<SSs> sss = await db.SSses.ToListAsync();
				return sss;
			}
			catch (Exception ex)
			{
				return new List<SSs>();
			}
		}
		public async Task<SSs> GetName(string name)
		{
			var cpc = await db.SSses.FirstOrDefaultAsync(c => c.StandardName == name);
			return cpc;
		}
		public async Task<SSs> GetID(int id)
		{
			var cpc = await db.SSses.FirstOrDefaultAsync(c => c.SsdId == id);
			return cpc;
		}
		/// <summary>
		/// 分页查询数据
		/// </summary>
		/// <param name="fenYeT"></param>
		/// <returns></returns>
		public async Task<FenYeT<SSs>> GetByFenYeAsync(FenYeT<SSs> fenYeT)
		{
			fenYeT.List = await db.SSses.Skip((fenYeT.PageIndex - 1) * fenYeT.PageSize).Take(fenYeT.PageSize).ToListAsync();
			fenYeT.DataCount = (await db.SSses.ToListAsync()).Count;
			return fenYeT;
		}
		/// <summary>
		/// 分页查询未经复核的人
		/// </summary>
		/// <param name="fenYeT"></param>
		/// <returns></returns>
		public async Task<FenYeT<SSs>> GetByFenYeByCheckStatusIsFalseAsync(FenYeT<SSs> fenYeT)
		{
			fenYeT.List = await db.SSses.Where(e => e.CheckStatus == 0).Skip((fenYeT.PageIndex - 1) * fenYeT.PageSize).Take(fenYeT.PageSize).ToListAsync();
			fenYeT.DataCount = (await db.SSses.Where(e => e.CheckStatus == 0).ToListAsync()).Count;
			return fenYeT;
		}

		/// <summary>
		/// 根据传入的 分页对象和 查询条件 分页地查询数据返回
		/// </summary>
		/// <param name="fenYeT"></param>
		/// <param name="condition"></param>
		/// <returns></returns>
		public async Task<FenYeT<SSs>> GetByFenYeByWhereAsync(FenYeT<SSs> fenYeT, string condition)
		{
			try
			{
				using (var sqlConnection = db.Database.GetDbConnection())
				{
					//调用分页的存储过程
					//@pageSize, @keyName, @tableName, @currentPage, @rows
					var parameters = new DynamicParameters();
					parameters.Add("pageSize", dbType: DbType.Int32, direction: ParameterDirection.Input, value: fenYeT.PageSize);//输入参
					parameters.Add("keyName", dbType: DbType.String, direction: ParameterDirection.Input, value: "SsdId");//输入参
					parameters.Add("where", dbType: DbType.String, direction: ParameterDirection.Input, value: condition);//输入参
					parameters.Add("lie", dbType: DbType.String, direction: ParameterDirection.Input, value: "*");//输入参
					parameters.Add("tableName", dbType: DbType.String, direction: ParameterDirection.Input, value: "SSses");//输入参
					parameters.Add("currentPage", dbType: DbType.Int32, direction: ParameterDirection.Input, value: fenYeT.PageIndex);//输入参
					parameters.Add("rows", dbType: DbType.Int32, direction: ParameterDirection.Output);//输出参
					string sql = "FYByWhere";
					IEnumerable<SSs> values = await sqlConnection.QueryAsync<SSs>(sql, parameters, commandType: CommandType.StoredProcedure);
					fenYeT.DataCount = parameters.Get<int>("@rows");
					if (values != null)
					{
						fenYeT.List = values.ToList();
					}
				}
				return fenYeT;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
		}


		/// <summary>
		/// 根据标准单编号获取基本信息
		/// </summary>
		/// <param name="standardId"></param>
		/// <returns></returns>
		public async Task<SSs> GetByStandardId(string standardId)
		{
			try
			{
				return await db.SSses.SingleOrDefaultAsync(e => e.StandardId == standardId);
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		/// <summary>
		/// 根据传入的数据进行修改
		/// </summary>
		/// <param name="sSs"></param>
		/// <param name="sSDs"></param>
		/// <returns></returns>
		public async Task<bool> UpdateSSsAsync(SSs sSs, List<SSDs> sSDs)
		{
			try
			{
				SSs ss = await db.SSses.SingleOrDefaultAsync(e => e.StandardId == sSs.StandardId);
				ss.StandardName = sSs.StandardName;
				ss.SalarySum = sSs.SalarySum;
				// 复核用到的数据
				if (sSs.CheckTime != null) { ss.CheckTime = sSs.CheckTime; }
				if (sSs.Checker != null) { ss.Checker = sSs.Checker; }
				if (sSs.CheckComment != null) { ss.CheckComment = sSs.CheckComment; }
				if (sSs.CheckStatus != 0 && sSs.CheckStatus != null) { ss.CheckStatus = sSs.CheckStatus; }
				// 变更用到的数据
				if (sSs.Changer != null && sSs.Changer != 0) { ss.Changer = sSs.Changer; }
				if (sSs.ChangeStatus != null && sSs.ChangeStatus != 0) { ss.ChangeStatus = sSs.ChangeStatus; }
				if (sSs.Remark != null && sSs.Remark != "") { ss.Remark = sSs.Remark; }
				if (sSs.ChangeTime != null) { ss.ChangeTime = sSs.ChangeTime; }
				List<SSDs> ssds = await db.SSDs.Where(e => e.StandardId == ss.StandardId).ToListAsync();
				foreach (var ssd in ssds)
				{
					foreach (var ssd1 in sSDs)
					{
						if (ssd.ItemId == ssd1.ItemId)
						{
							ssd.Salary = ssd1.Salary;
							// 标记实体为已修改状态
							db.Entry(ssd).State = EntityState.Modified;
						}
					}
				}
				return await db.SaveChangesAsync() > 0;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}
		public async Task<SSs> GetSID(string sId)
		{
			using (var dbContext = db) // 替换为您的 DbContext 类
			{
				var ss = await dbContext.SSses.FirstOrDefaultAsync(s => s.StandardId == sId);
				return ss;
			}
		}
	}
}
