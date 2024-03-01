using EFCore.Models;
using HRIServices;
using Microsoft.AspNetCore.Mvc;

namespace HRUI.Controllers
{
	public class CFFKsController : Controller
	{
		private readonly ICFFKsService cFFKsService;

		public CFFKsController(ICFFKsService cFF)
		{
			this.cFFKsService = cFF;
		}
		/// <summary>
		/// 查询展示一级标签
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> First_kind()
		{
			ViewData["getCFFKs"] = await cFFKsService.GetAllAsync();
			return View();
		}
		
		/// <summary>
		/// 点击变更时展示变更操作页面
		/// </summary>
		/// <param name="FfkId"></param>
		/// <returns></returns>
		public async Task<IActionResult> First_kind_change(int ffkId)
		{
			return View(await cFFKsService.GetByFfkIdAsync(ffkId));
		}
		/// <summary>
		/// 点击添加时进行页面变更操作
		/// </summary>
		/// <returns></returns>
		public IActionResult First_kind_register()
		{
			return View();
		}
		/// <summary>
		/// 进行添加操作，添加成功后跳转到成功界面，否则返回
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> First_kind_register_success(CFFKs CFFKs)
		{
			CFFKs.FirstKindId = Guid.NewGuid().ToString();
			if (await cFFKsService.AddAsync(CFFKs))
			{
				return View();
			}
			else
			{
				ViewData["isOk"] = "unOk";
				return View("First_kind_register");
			}
		}
		/// <summary>
		/// 进行修改并且判断修改是否成功
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> First_kind_change_success(CFFKs CFFKs)
		{
			if (await cFFKsService.UpdateAsync(CFFKs))
			{
				return View();
			}
			else
			{
				ViewData["isOk"] = "unOk";
				return View("First_kind_change");
			}
		}
		/// <summary>
		/// 根据传入的id进行删除
		/// </summary>
		/// <param name="ffkId"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<IActionResult> First_kind_delete(int ffkId)
		{
			if(await cFFKsService.DeleteAsync(ffkId))
			{
				ViewData["getCFFKs"] = await cFFKsService.GetAllAsync();
				return View("First_kind");
			}
			else
			{
				return Content("false");
			}
		}
	}
}
