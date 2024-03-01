using Dapper;
using EFCore;
using EFCore.Models;
using HRIServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRServices
{
	public class UsersService : IUsersService
    {
        private readonly MyDbContext db;

        public UsersService(MyDbContext db)
        {
            this.db = db;
        }
        /// <summary>
        /// 查询所有的用户
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
		public async Task<List<Users>> GetAll()
		{
			return await db.Users.ToListAsync();
		}

		public async Task<Users> LoginAsync(Users users)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(db.Database.GetConnectionString()))
                {
                    string sql = $"SELECT * FROM Users WHERE UName COLLATE Latin1_General_CS_AS = '{users.UName}' AND UPassWord COLLATE Latin1_General_CS_AS = '{users.UPassWord}'";
                    Users user = (await sqlcon.QueryAsync<Users>(sql)).ToList()[0] ;
                    return user;
                }
                    
            }catch (Exception ex)
            {
                return null;
            }
        }

        public void Test()
        {
            Console.WriteLine("成功");
        }
    }
}
