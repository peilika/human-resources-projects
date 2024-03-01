using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
	public interface IPowersService
	{
		//用户管理
		Task<List<Users>> User_ListSelect();//用户管理查询
		Task<List<Users>> User_ListSelect(FenYePage page);//分页查询
		Task<int> User_ListCount();//查询总数据
		Task<Users> User_Edit(int id);//修改（点击进入修改页面）
		Task<bool> User_AddInsert(Users users);//添加用户管理
        Task<int> User_ListUID();//查询最新添加的数据ID
		Task<bool> User_RemoveInsert(int uid, int urid);//删除用户
		Task<bool> User_Edit(Users users);//修改


		//用户角色
		Task<bool> User_AddInsert2(UserRoles userRoles);//添加用户角色
        Task<List<UserRoles>> User_ListSelect2();//查询用户角色
		Task<UserRoles> UserRoles_Edit(int id);//修改（点击进入修改页面）
		Task<bool> UserRoles_Edits(UserRoles userRoles);//修改


		//角色管理
		Task<List<Roles>> Right_ListSelect();//角色管理查询
		Task<List<Roles>> GetRight_ListSelect();
		Task<List<Roles>> Right_ListSelect(FenYePage page);//分页查询
        Task<List<Roles>> Right_ListSelect2();//角色查询（是否可用）
		Task<int> Right_ListCount();//查询总数据
		Task<bool> Right_AddInsert(Roles role);//添加角色管理
		Task<bool> Right_ListDelete(int id);//删除角色管理
		Task<Roles> Right_list_information(int id);//进入修改页面
		Task<int> GetRolesID(int id);
		Task<Roles> GetRoles(int id);

		Task<bool> RoJurDelete(int id);//删除角色权限
		Task<bool> RJurInsert(RolesJurisdictions rjur);//添加角色权限
		Task<List<RolesJurisdictions>> GetJuriIDsByRolesID(int RolesID);//默认选中
		Task<List<Jurisdictions>> GetJuriIDsByID(List<RolesJurisdictions> rjur);//默认选中2


		Task<int> GetUid(int uid);//根据用户Id查询用户角色Id
		Task<bool> Right_ListUpdate(Roles role);//修改角色信息
		Task<List<int>> GetGid2(int rid);//根据用户Id查询用户权限
	}
}
