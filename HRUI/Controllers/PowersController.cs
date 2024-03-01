using EFCore.Models;
using HRIServices;
using HRServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HRUI.Controllers
{
	//用户角色管理
	public class PowersController:Controller
	{
		private readonly IPowersService powersService;
		private readonly IPowersService powersServices;

		public PowersController(IPowersService powersService, IPowersService powersServices)
		{
			this.powersService = powersService;
			this.powersServices = powersServices;
		}
		//分页查询
		public async Task<ActionResult> User_List(FenYePage page)
		{
            if (page.PageSize==0&&page.PageIndex==0)
            {
                // 如果 page 参数为 null，则设置默认值
                page = new FenYePage { PageIndex = 1, PageSize = 5 };
            }

            List<Users> list = await powersService.User_ListSelect(page);
			ViewData["Page"] = page;


            List<UserRoles> list2 = await powersService.User_ListSelect2();
			
            ViewData["UserSelect"] = list;
            ViewData["UserCount"] = await User_ListCount();
            ViewData["UserRolesSelect"] = list2;
            ViewData["UserRolesSelect2"] = await Right_List2();

            return View();
        }
		// 查询用户管理总数
		private async Task<int> User_ListCount()
		{
			int count = await powersService.User_ListCount();
			return count;
		}
        //进入添加用户界面
        [HttpGet]
        public async Task<ActionResult> User_Add()
        {
			ViewData["Right_ListSelect"] = Right_List2().Result;
            return View();
        }
		//点击进入修改页面
		[HttpGet]
		public async Task<ActionResult> User_Edit(int id)
		{
			Users user = await powersService.User_Edit(id);
			ViewData["User_EditSelect"] = user;
			Roles role = await powersService.GetRoles(GetRolesID(id).Result);
			ViewData["Roles"] = role;
			ViewData["Right_ListSelect"] = Right_List2().Result;
			return View();
		}
		private async Task<int> GetRolesID(int id)
		{
			int roles = await powersService.GetRolesID(id);
			return roles;
		}
		//修改
		[HttpPost]
		public async Task<ActionResult> User_EditUpdate(Users user,UserRoles userRoles,FenYePage page)
		{
			var zhi = await powersService.User_Edit(user);
			var zhi2 = await powersService.UserRoles_Edits(userRoles);
			if (zhi&&zhi2)
			{
				if (page.PageSize == 0 && page.PageIndex == 0)
				{
					// 如果 page 参数为 null，则设置默认值
					page = new FenYePage { PageIndex = 1, PageSize = 5 };
				}

				List<Users> list = await powersService.User_ListSelect(page);
				ViewData["Page"] = page;


				List<UserRoles> list2 = await powersService.User_ListSelect2();

				ViewData["UserSelect"] = list;
				ViewData["UserCount"] = await User_ListCount();
				ViewData["UserRolesSelect"] = list2;
				ViewData["UserRolesSelect2"] = await Right_List2();

				return View("User_List");
			}
			else
			{
				return await User_Edit(user.UId);
			}
			
		}
		//进入修改页面
		private async Task<UserRoles> UserRoles_Edit(int id)
		{
			UserRoles userRoles = await powersService.UserRoles_Edit(id);
			return userRoles;
		}
		//添加用户
		public async Task<ActionResult> User_AddInsert(Users users,UserRoles userRoles)
		{	
			bool zhi = await powersService.User_AddInsert(users);
            UserRoles userRoles1 = new UserRoles()
            {
                UserID = User_ListUID().Result,
                RolesID = userRoles.RolesID
            };
			User_AddInset2(userRoles1);
            if (zhi)
			{
				return RedirectToAction("User_List");
			}
			else
			{
				return View();
			}
		}
		//添加用户角色
		public async Task<ActionResult> User_AddInset2(UserRoles userRoles)
		{
			bool zhi = await powersService.User_AddInsert2(userRoles);
			if (zhi)
            {
                return RedirectToAction("User_List");
            }
            else
            {
                return View();
            }
        }
		// 获取最新的UID
		private async Task<int> User_ListUID()
		{
			int uid = await powersService.User_ListUID();
			return uid;
		}
        //分页查询
        public async Task<ActionResult> Right_List(FenYePage page)
        {
            if (page.PageSize == 0 && page.PageIndex == 0)
            {
                // 如果 page 参数为 null，则设置默认值
                page = new FenYePage { PageIndex = 1, PageSize = 5 };
            }

            List<Roles> list = await powersService.Right_ListSelect(page);
			ViewData["Page"] = page;
            ViewData["RightSelect"] = list;
            ViewData["RightCount"] = Right_ListCount().Result;

            return View();
        }
        //查询角色
        private async Task<List<Roles>> Right_List2()
		{
			List<Roles> list = await powersService.Right_ListSelect();
			return list;
		}
		// 查询角色总数
		private async Task<int> Right_ListCount()
		{
            int count = await powersServices.Right_ListCount();
            return count;
        }
		// 点击进入添加角色界面
		[HttpGet]
		public async Task<ActionResult> Right_Add()
		{
            return View();
        }
		// 添加角色
		[HttpPost]
		public async Task<ActionResult> Right_Add(Roles role)
		{
            bool zhi = await powersService.Right_AddInsert(role);
            if (zhi)
            {
                return RedirectToAction("Right_List");
            }
            else
            {
                return View();
            }
        }
		///删除角色
		public async Task<ActionResult> Right_ListDelete(int id,FenYePage page)
		{
			if (await powersService.Right_ListDelete(id))
			{
				if (page.PageSize == 0 && page.PageIndex == 0)
				{
					// 如果 page 参数为 null，则设置默认值
					page = new FenYePage { PageIndex = 1, PageSize = 5 };
				}

				List<Roles> list = await powersService.Right_ListSelect(page);
				ViewData["Page"] = page;
				ViewData["RightSelect"] = list;
				ViewData["RightCount"] = Right_ListCount().Result;

				return View("Right_List");
            }
			else
			{
				return Content("false");
			}
        }
		public async Task<ActionResult> GetRList()
		{
			return Json(await powersService.GetRight_ListSelect());
		}
		//删除用户角色
		public async Task<ActionResult> User_RemoveInsert(int uid,int urid,FenYePage page)
		{
			if(await powersService.User_RemoveInsert(uid, urid))
			{
				if (page.PageSize == 0 && page.PageIndex == 0)
				{
					// 如果 page 参数为 null，则设置默认值
					page = new FenYePage { PageIndex = 1, PageSize = 5 };
				}

				List<Users> list = await powersService.User_ListSelect(page);
				ViewData["Page"] = page;

				List<UserRoles> list2 = await powersService.User_ListSelect2();

				ViewData["UserSelect"] = list;
				ViewData["UserCount"] = await User_ListCount();
				ViewData["UserRolesSelect"] = list2;
				ViewData["UserRolesSelect2"] = await Right_List2();

				return View("User_List");
			}
			else
			{
				return Content("fales");
			}
		}
		//点击进入修改页面
		[HttpGet]
		public async Task<ActionResult> Right_list_information(int id)
		{
			Roles roles = await powersService.Right_list_information(id);
			ViewData["list"] = roles;
			ViewData["Right_ListSelect"] = Right_List2().Result;
			ViewData["UserRoles_EditSelect"] = UserRoles_Edit(id).Result;

			List<RolesJurisdictions> rJur = await powersServices.GetJuriIDsByRolesID(id);
			List<Jurisdictions> jur = await powersServices.GetJuriIDsByID(rJur);
			ViewData["JurID"] = jur;
			return View();
		}
		//修改角色信息和权限
		[HttpPost]
		public async Task<ActionResult> Right_list_information(string Jurisdiction, int rid, FenYePage page,Roles role)
		{
			if (Jurisdiction != null)
			{
				await powersService.RoJurDelete(rid);
				string[] jur = Jurisdiction.Split(',');
				for (int i = 0; i < jur.Length; i++)
				{
					if (int.TryParse(jur[i], out int result))
					{
						RolesJurisdictions rjur = new RolesJurisdictions()
						{
							RolesID = rid,
							JuriID = result
						};
						await powersServices.RJurInsert(rjur);
					}
				}
			}

			await powersServices.Right_ListUpdate(role);

			if (page.PageSize == 0 && page.PageIndex == 0)
			{
				// 如果 page 参数为 null，则设置默认值
				page = new FenYePage { PageIndex = 1, PageSize = 5 };
			}
			List<Roles> list = await powersServices.Right_ListSelect(page);
			ViewData["Page"] = page;
			ViewData["RightSelect"] = list;
			ViewData["RightCount"] = Right_ListCount().Result;
			return View("Right_List");
			
		}
	}
}
