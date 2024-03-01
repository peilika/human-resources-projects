using EFCore.Models;
using HRIServices;
using HRServices;
using Microsoft.AspNetCore.Mvc;

namespace HRUI.Controllers
{
	public class SalarysController:Controller
	{
		private readonly IPYsService pYsService;
		private readonly ICPCsService cPCsService;

		public SalarysController(IPYsService pYsService, ICPCsService cPCsService)
		{
			this.pYsService = pYsService;
			this.cPCsService = cPCsService;
		}
		//查询
		public async Task<ActionResult> Salary_item()
		{
			List<CPCs> listCPC = await cPCsService.Public_char();
			ViewData["CPCList"] = listCPC;
			return View();
		}
		//点击进入添加页面www
		[HttpGet]
		public async Task<ActionResult> Salary_item_Register()
		{
			return View();
		}
		//添加
		[HttpPost]
		public async Task<ActionResult> Salary_item_Register(CPCs item)
		{
			bool zhi = await cPCsService.Public_char_Add(item);
			if (zhi)
			{
				return RedirectToAction("Salary_item");
			}
			else
			{
				return View();
			}
		}
		//删除
		public async Task<ActionResult> Salary_Remove(int id)
		{
			if (await cPCsService.Public_char_Delete(id))
			{
				List<CPCs> listCPC = await cPCsService.Public_char();
				ViewData["CPCList"] = listCPC;
				return View("Salary_item");
			}
			else
			{
				return Content("false");
			}
		}
	}
}
