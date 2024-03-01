using EFCore.Models;
using HRIServices;
using HRServices;
using Microsoft.AspNetCore.Mvc;

namespace HRUI.Controllers
{
	public class CFSKsController : Controller
	{
		private readonly ICFSKsService CfsksService;
		private readonly ICFFKsService CFFKsService;
		public CFSKsController(ICFSKsService cfsksService, ICFFKsService cFFKsService)
		{
			CfsksService = cfsksService;
			CFFKsService = cFFKsService;
		}

		/// <summary>
		/// 查询数据返回至查询页面
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Second_kind()
		{
			ViewData["cfsks"] = await CfsksService.GetAllAsync();
			return View();
		}

		/// <summary>
		/// 打开二级标签的添加页面(加载一级标签的选项)
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Second_kind_register()
		{
			List<CFSKs> cFSKs = new List<CFSKs>();
			foreach (CFFKs cFFKs in await CFFKsService.GetAllAsync())
			{
				cFSKs.Add(new CFSKs()
				{
					FirstKindId = cFFKs.FirstKindId,
					FirstKindName = cFFKs.FirstKindName,
				});
			}

			ViewData["cfsks"] = cFSKs;
			return View();
		}
		/// <summary>
		/// 进行添加操作
		/// </summary>
		/// <param name="cFSKs"></param>
		/// <returns></returns>
		public async Task<IActionResult> Second_kind_register_success(CFSKs cFSKs)
		{
			CFFKs c = await CFFKsService.GetByFirstKindIdAsync(cFSKs.FirstKindId);
			cFSKs.SecondKindId = Guid.NewGuid().ToString();
			cFSKs.FirstKindName = c.FirstKindName;
			if (await CfsksService.AddAsync(cFSKs))
			{
				return View();
			}
			else
			{
				ViewData["isOk"] = "unOk";
				return View("Second_kind_register");
			}
		}
		/// <summary>
		/// 打开修改界面
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Second_kind_change(int fskId)
		{
			return View(await CfsksService.GetByFskIdAsync(fskId));	
		}
		/// <summary>
		/// 进行修改操作
		/// </summary>
		/// <param name="cfsks"></param>
		/// <returns></returns>
		public async Task<IActionResult> Second_kind_change_success(CFSKs cfsks)
		{
			if (await CfsksService.UpdateAsync(cfsks))
			{
				return View();
			}
			else
			{
				ViewData["isOk"] = "unOK";
				return View("Second_kind_change", await CfsksService.GetBySecondKindIdAsync(cfsks.SecondKindId));
			}
		}
		/// <summary>
		/// 根据传入的二级编号进行删除
		/// </summary>
		/// <param name="fskId"></param>
		/// <returns></returns>
		public async Task<IActionResult> Second_kind_delete(int fskId)
		{
			if (await CfsksService.DeleteAsync(fskId))
			{
				ViewData["cfsks"] = await CfsksService.GetAllAsync();
				return View("Second_kind");
			}
			else
			{
				return Content("false");
			}
		}
	}
}
