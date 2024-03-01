using Dapper;
using EFCore;
using EFCore.Models;
using HRIServices;
using HRUI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRServices
{
	public class SGservice : ISGService
	{
		private readonly MyDbContext db;
		public SGservice(MyDbContext db)
		{
			this.db = db;
		}
		public async Task<FenYeT<SG>> GetByFenYeByCheckStatusIsFalseAsync(FenYeT<SG> fenYeT)
		{
			fenYeT.List = await db.sGs.Where(e => e.CheckStatus == false || e.CheckStatus == null).Skip((fenYeT.PageIndex - 1) * fenYeT.PageSize).Take(fenYeT.PageSize).ToListAsync();
			fenYeT.DataCount = (await db.sGs.Where(e => e.CheckStatus == false || e.CheckStatus == null).ToListAsync()).Count;
			return fenYeT;
		}

		public async Task<List<SGD2>> sb(string n)
		{
			using (var conn = new SqlConnection("Data Source=.;Initial Catalog=HR;Integrated Security=True;Encrypt=False;"))
			{
				conn.Open();
				var result = await conn.QueryAsync<SGD2>("SELECT * FROM SGD2 WHERE SalaryGrantId = @n", new { n });
				return result.ToList();
			}
		}

		public async Task<List<SSDs>> sb1(string n)
		{
			using (var conn = new SqlConnection("Data Source=.;Initial Catalog=HR;Integrated Security=True;Encrypt=False;"))
			{
				conn.Open();
				var result = await conn.QueryAsync<SSDs>("SELECT * FROM SSDs WHERE StandardId = @n", new { n });
				return result.ToList();
			}
		}
	}
}
