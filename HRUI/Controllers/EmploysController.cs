using EFCore.Models;
using HRIServices;
using Microsoft.AspNetCore.Mvc;

namespace HRUI.Controllers
{
	public class EmploysController : Controller
	{
		private readonly IHumanResourcesService humanResources;
		public EmploysController(IHumanResourcesService humanResources)
		{
			this.humanResources = humanResources;
		}
		public async Task<ActionResult> Lists(FenYePage page)
		{
			if (page.PageSize == 0 && page.PageIndex == 0)
			{
				// 如果 page 参数为 null，则设置默认值
				page = new FenYePage { PageIndex = 1, PageSize = 5 };
			}
			List<ERs> list = await humanResources.Lists_ER(page);
			ViewData["Page"] = page;
			ViewData["List"] = list;
			ViewData["Count"] = list.Count;
			return View("Lists");
		}
	}
}
