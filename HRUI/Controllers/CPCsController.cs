using EFCore.Models;
using HRIServices;
using HRServices;
using HRUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace HRUI.Controllers
{
	public class CPCsController : Controller
	{
		private readonly ICPCsService cPCsService;

		public CPCsController(ICPCsService cpc)
		{
			this.cPCsService = cpc;
		}
		//查询属性
		public async Task<ActionResult> Public_char()
		{
			List<CPCs> list = await cPCsService.Public_char();
			ViewData["list"] = list;
			ViewData["list2"] = Public_char_cmks().Result;
			return View();
		}
		//查询种类
		private async Task<List<CMKs>> Public_char_cmks()
		{
			List<CMKs> list = await cPCsService.Public_char_cmks();
			return list;
		}
		//点击进入添加页面
		[HttpGet]
		public async Task<ActionResult> Public_char_Add()
		{
			return View();
		}
		//添加
		[HttpPost]
		public async Task<ActionResult> Public_char_Add(CPCs cp)
		{
			var zhi = await cPCsService.Public_char_Add(cp);
			if (zhi)
			{
				List<CPCs> list = await cPCsService.Public_char();
				ViewData["list"] = list;
				ViewData["list2"] = Public_char_cmks().Result;
				return View("Public_char");
			}
			else
			{
				return View();
			}
		}
		//删除
		public async Task<ActionResult> Public_char_Delete(int id)
		{
			if (await cPCsService.Public_char_Delete(id))
			{
				List<CPCs> list = await cPCsService.Public_char();
				ViewData["list"] = list;
				ViewData["list2"] = Public_char_cmks().Result;
				return View("Public_char");
			}
			else
			{
				return Content("false");
			}
		}
		// 根据传入的数组获取对应的类型项
		[HttpGet]
		public async Task<IActionResult> GetBySelectedOptions(string data)
		{
			List<CPCsAndSSDs> list = new List<CPCsAndSSDs>();
			int[] ints = Array.ConvertAll(data.Split(','), int.Parse);
			foreach(var a in await cPCsService.GetByPbcIds(ints))
			{
				CPCsAndSSDs cPCsAndSSDs = new CPCsAndSSDs()
				{
					PbcId = a.PbcId,
					AttributeKind = a.AttributeKind,
					AttributeName = a.AttributeName,
					Salary = 0.0
				};
				list.Add(cPCsAndSSDs);
			}
			return Json(list);
		}
	}
}
