using EFCore.Models;
using HRIServices;
using HRServices;
using HRUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HRUI.Controllers
{
	public class ClientsController:Controller
	{
		private readonly ICPCsService cPCsService;
		private readonly IClientsService clientsService;

		public ClientsController(IClientsService clientsService,ICPCsService cpc)
		{
			this.clientsService = clientsService;
			this.cPCsService = cpc;
		}
		//查询职称
		public async Task<ActionResult> Profession_design()
		{
			List<CPCs> list = await cPCsService.Public_char();
			ViewData["list"] = list;
			return View();
		}
		//删除职称
		public async Task<ActionResult> ClientDelete(int id)
		{
			if (await cPCsService.Public_char_Delete(id))
			{
				List<CPCs> list = await cPCsService.Public_char();
				ViewData["list"] = list;
				return View("Profession_design");
			}
			else
			{
				return Content("false");
			}
		}



		//职位
		//查询职位
		public async Task<ActionResult> Major_kind()
		{
			List<CMKs> major = await clientsService.MajorSelect();
			ViewData["list"] = major;
			return View();
		}
		//删除职位分类
		public async Task<ActionResult> Major_Delete(int id)
		{
			if (await clientsService.MajorDelete(id))
			{
				List<CMKs> major = await clientsService.MajorSelect();
				ViewData["list"] = major;
				return View("Major_kind");
			}
			else
			{
				return Content("false");
			}
		}



		//职位分类
		//页面跳转
		[HttpGet]
		public async Task<ActionResult> Major_kind_add()
		{
			return View();
		}
		[HttpPost]
		public async Task<ActionResult> Major_kind_add(CMKs cM)
		{
			List<int> count = SeletCounts().Result;
			int majorID = 0;
			if (count.Count > 0)
			{
				int maxCount = count.Max();
				majorID = maxCount;
			}
			bool zhi = await clientsService.MajorInsert(cM, majorID);
			if (zhi)
			{
				return RedirectToAction("Major_kind");
			}
			else
			{
				return View();
			}
		}
		private async Task<List<int>> SeletCounts()
		{
			List<int> counts = await clientsService.SelectCount();
			return counts;
		}


		//职位设置表
		//查询
		public async Task<ActionResult> Major()
		{
			List<CMs> cm = await clientsService.MajorS();
			ViewData["list"] = cm;
			return View();
		}
		[HttpGet]
		public async Task<ActionResult> Major_Add()
		{
			List<CMKs> major = await clientsService.MajorSelect();
			ViewData["list"] = major;
			return View();
		}
		[HttpPost]
		public async Task<ActionResult> Major_Add(CMs cms)
		{
			//bool zhi = await clientsService.MajorA(cms);
			//if (zhi)
			//{
			//	List<CMs> cm = await clientsService.MajorS();
			//	ViewData["list"] = cm;
			//	return View("Major");
			//}
			//else
			//{
			//	return View();
			//}

			List<int> count = SeletCountTwo().Result;
			int majorID = 0;
			if (count.Count > 0)
			{
				int maxCount = count.Max();
				majorID = maxCount;
			}
			bool zhi = await clientsService.MajorA(cms, majorID);
			if (zhi)
			{
				return RedirectToAction("Major");
			}
			else
			{
				return View();
			}
		}
		private async Task<List<int>> SeletCountTwo()
		{
			List<int> counts = await clientsService.SelectCount2();
			return counts;
		}
		public async Task<ActionResult> Major_D(int id)
		{
			if (await clientsService.MajorD(id))
			{
				List<CMs> major = await clientsService.MajorS();
				ViewData["list"] = major;
				return View("Major");
			}
			else
			{
				return Content("false");
			}
		}
	}
}
