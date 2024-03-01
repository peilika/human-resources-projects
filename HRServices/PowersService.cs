using EFCore;
using EFCore.Models;
using HRIServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRServices
{
	public class PowersService:IPowersService
	{
		private readonly MyDbContext db;
		private readonly MyDbContext dbs;

		public PowersService(MyDbContext db, MyDbContext dbs)
		{
			this.db = db;
			this.dbs = dbs;
		}
		//查询用户管理
		public async Task<List<Users>> User_ListSelect()
		{
			return await db.Users.ToListAsync();
		}
		//分页查询
		public async Task<List<Users>> User_ListSelect(FenYePage page)
		{
            int pageSize = page.PageSize;
            int pageIndex = page.PageIndex;

            var users = await db.Users
                .Skip((pageIndex - 1) * pageSize) // 跳过前面的数据项
                .Take(pageSize) // 获取指定数量的数据项
                .ToListAsync();

            return users;
        }
		// 查询用户管理总数
		public Task<int> User_ListCount()
		{
			return db.Users.CountAsync();
		}
		// 添加用户管理
		public async Task<bool> User_AddInsert(Users users)
		{
			db.Users.Add(users);
			return await db.SaveChangesAsync() > 0;
		}
		// 查询最新的数据
		public async Task<int> User_ListUID()
		{
            var latestEntity = await db.Users.OrderByDescending(e => e.UId).FirstOrDefaultAsync();
            int latestID = latestEntity?.UId ?? 0;
			return latestID;
        }
		//删除用户
		public async Task<bool> User_RemoveInsert(int uid, int urid)
		{
            db.Users.Remove(await db.Users.FindAsync(uid));
            db.UserRoles.Remove(await db.UserRoles.FindAsync(urid));
            return db.SaveChanges() > 0;
        }
		//修改（点击进入修改页面）
		public async Task<Users> User_Edit(int id)
		{
			return await db.Users.FindAsync(id);
		}
		//修改
		public async Task<bool> User_Edit(Users users)
		{
			Users users1 = await db.Users.FindAsync(users.UId);
			users1.UName = users.UName;
			users1.UTrueName = users.UTrueName;
			users1.UPassWord = users.UPassWord;
			return db.SaveChanges()>0;
		}




		// 添加用户角色
		public async Task<bool> User_AddInsert2(UserRoles userRoles)
		{
            db.UserRoles.Add(userRoles);
            return await db.SaveChangesAsync() > 0;
        }
        //查询用户角色
        public async Task<List<UserRoles>> User_ListSelect2()
        {
            return await db.UserRoles.ToListAsync();
        }
		//修改（点击进入修改页面）
		public async Task<UserRoles> UserRoles_Edit(int id)
		{
			return await db.UserRoles.FindAsync(id);
		}
		//修改用户角色
		public async Task<bool> UserRoles_Edits(UserRoles userRoles)
		{
			UserRoles userRoles1 = await db.UserRoles.FindAsync(userRoles.UserRolesID);
			userRoles1.UserID = userRoles.UserID;
			userRoles1.RolesID = userRoles.RolesID;
			return db.SaveChanges() > 0;
		}
		public async Task<int> GetRolesID(int id) 
		{
			var role = await db.UserRoles.Where(ur => ur.UserRolesID == id).Select(ur => ur.RolesID).FirstOrDefaultAsync();
			return role;
		}
		public async Task<Roles> GetRoles(int id)
		{
			var role = await db.Roles.FirstOrDefaultAsync(r => r.RolesID == id);
			return role;
		}


		// 查询角色管理
		public async Task<List<Roles>> Right_ListSelect()
        {
            return await db.Roles.ToListAsync();
        }
		public async Task<List<Roles>> GetRight_ListSelect()
		{
			var roles = await db.Roles.Where(r => r.RolesIf == 1).ToListAsync();
			return roles;
		}
		//分页查询
		public async Task<List<Roles>> Right_ListSelect(FenYePage page)
        {
            int pageSize = page.PageSize;
            int pageIndex = page.PageIndex;

            var roles = await db.Roles
                .Skip((pageIndex - 1) * pageSize) // 跳过前面的数据项
                .Take(pageSize) // 获取指定数量的数据项
                .ToListAsync();

            return roles;
        }
        //查询角色是否可用
        public async Task<List<Roles>> Right_ListSelect2()
		{
			return await db.Roles.Where(r => r.RolesIf == 1).ToListAsync();
		}
		// 查询用角色管理总数
		public Task<int> Right_ListCount()
        {
            return db.Roles.CountAsync();
        }
		//添加角色管理
		public async Task<bool> Right_AddInsert(Roles role)
		{
			db.Roles.Add(role);
			return await db.SaveChangesAsync()>0;
		}
		//删除角色管理
		public async Task<bool> Right_ListDelete(int id)
		{
            db.Roles.Remove(await db.Roles.FindAsync(id));
            return db.SaveChanges() > 0;
        }
		//进入角色权限修改界面
		public async Task<Roles> Right_list_information(int id)
		{
			return await db.Roles.FindAsync(id);
		}
		

		//修改时先删除已有权限
		public async Task<bool> RoJurDelete(int id)
		{
			try { 
				using (var context = db)
				{
					var rolesJurisdictions = await context.RolesJurisdictions
						.Where(rj => rj.RolesID == id)
						.ToListAsync();

					if (rolesJurisdictions.Count > 0)
					{
						context.RolesJurisdictions.RemoveRange(rolesJurisdictions);
						await context.SaveChangesAsync();
						return true; // 删除成功
					}
					else
					{
						return false; // 未找到对应ID的数据
					}
				}
			}
			catch (Exception ex)
			{
				// 处理异常
				Console.WriteLine(ex.Message);
				return false; // 删除失败
			}
		}
		//修改用户角色信息
		public async Task<bool> Right_ListUpdate(Roles role)
		{
			Roles roles = await db.Roles.FindAsync(role.RolesID);
			if (roles.RolesName==role.RolesName&&roles.RolesInstruction==role.RolesInstruction&&roles.RolesIf==role.RolesIf)
			{
				return true;
			}
			else
			{
				roles.RolesName = role.RolesName;
				roles.RolesInstruction = role.RolesInstruction;
				roles.RolesIf = role.RolesIf;
				return db.SaveChanges() > 0;
			}
		}
		//添加
		public async Task<bool> RJurInsert(RolesJurisdictions rjur)
		{
			bool zhi = true;
			try
			{
				db.RolesJurisdictions.Add(rjur);
				zhi = await db.SaveChangesAsync() > 0;
			}catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			
			return zhi;
		}
		//默认选中
		public async Task<List<RolesJurisdictions>> GetJuriIDsByRolesID(int rolesID)
		{
			using (var context = db) // 请替换 YourDbContext 为实际的 DbContext 类型
			{
				List<RolesJurisdictions> result = await context.RolesJurisdictions
					.Where(rj => rj.RolesID == rolesID)
					.ToListAsync();

				return result;
			}
		}
		//选取子节点
		public async Task<List<Jurisdictions>> GetJuriIDsByID(List<RolesJurisdictions> rjur)
		{
			List<Jurisdictions> result = new List<Jurisdictions>();
			try
			{
				using (var context = dbs) 
				{
					List<int> juriIDs = rjur.Select(rj => rj.JuriID).ToList();

					result = await context.Jurisdictions
						.Where(j => juriIDs.Contains(j.JuriID) && j.GroupID == 3)
						.ToListAsync();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			return result;
		}
		//获取角色ID
		public async Task<int> GetUid(int uid)
		{
			using (var context = db)
			{
				var userRole = await context.UserRoles.FirstOrDefaultAsync(ur => ur.UserRolesID == uid);
				if (userRole != null)
				{
					return userRole.RolesID;
				}
				else
				{
					return 0;
				}
			}
		}
		//获取权限ID
		public async Task<List<int>> GetGid2(int rid)
		{
			using (var context = dbs)
			{
				var juriIds = await context.RolesJurisdictions
					.Where(rj => rj.RolesID == rid)
					.OrderBy(rj => rj.JuriID)                                                                                                                                          
					.Select(rj => rj.JuriID)
					.ToListAsync();
				return juriIds;
			}
		}
	}
}
