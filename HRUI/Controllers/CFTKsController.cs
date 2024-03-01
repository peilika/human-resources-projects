using EFCore.Models;
using HRIServices;
using HRServices;
using HRUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRUI.Controllers
{
	public class CFTKsController : Controller
	{
		private readonly ICFTKsService CFTKsService;
		private readonly ICFFKsService CFFKService;
		private readonly ICFSKsService CFSKsService;
		public CFTKsController(ICFTKsService cFTKsService, ICFFKsService fFKService, ICFSKsService cFSKsService)
		{
			this.CFTKsService = cFTKsService;
			this.CFFKService = fFKService;
			this.CFSKsService = cFSKsService;
		}
		/// <summary>
		/// 加载页面
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Third_kind()
		{
			ViewData["cftks"] = await CFTKsService.GetAllAsync();
			return View();
		}
		/// <summary>
		/// 打开添加页面
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Third_kind_register()
		{
			ViewData["cffks"] = await CFFKService.GetAllAsync();
			return View();
		}
		/// <summary>
		/// 进行添加操作
		/// </summary>
		/// <param name="cFTKs"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Third_kind_register_success(CFTKs cFTKs)
		{
			cFTKs.FirstKindName = (await CFFKService.GetByFirstKindIdAsync(cFTKs.FirstKindId)).FirstKindName;
			cFTKs.SecondKindName = (await CFSKsService.GetBySecondKindIdAsync(cFTKs.SecondKindId)).SecondKindName;
			cFTKs.ThirdKindId = Guid.NewGuid().ToString();
			if (await CFTKsService.AddAsync(cFTKs))
			{
				return View();
			}
			else
			{
				ViewData["isOk"] = "unOk";
				return View("Third_kind_register");
			}
		}
		/// <summary>
		/// 打开修改界面
		/// </summary>
		/// <param name="ftkId"></param>
		/// <returns></returns>
		public async Task<IActionResult> Third_kind_change(int ftkId)
		{
			var a = await CFTKsService.GetByFtkId(ftkId);
			ViewData["thirdKindIsRetail"] = a.ThirdKindIsRetail+"";
			return View(a);
		}
		/// <summary>
		/// 执行修改操作
		/// </summary>
		/// <param name="cFTKs"></param>
		/// <returns></returns>
		public async Task<IActionResult> Third_kind_change_success(CFTKs cFTKs)
		{                            
			if (await CFTKsService.UpdateAsync(cFTKs))
			{
				return View();
			}
			else 
			{
				ViewData["isOk"] = "unOk";
				return View("Third_kind_change");
			}
		}
		/// <summary>
		/// 进行删除操作
		/// </summary>
		/// <param name="ftkId"></param>
		/// <returns></returns>
		public async Task<IActionResult> Third_kind_delete(int ftkId)
		{
			if (await CFTKsService.DeleteAsync(ftkId))
			{
				ViewData["cftks"] = await CFTKsService.GetAllAsync();
				return View("Third_kind");
			}
			else
			{
				return Content("false");
			}
		}
		/// <summary>
		/// 获取所有的以及和二级标签的数据关系
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GetCFFKsAndCFSKs()
		{
			List<CFK> cFFKAndCFSKs = new List<CFK>();
			// 查询一级
			List<CFFKs> cFFKs = await CFFKService.GetAllAsync();
			// 查询二级
			List<CFSKs> cFSKs = await CFSKsService.GetAllAsync();
			// 循环合并
			foreach (CFFKs f in cFFKs)
			{
				CFK cFFKAndCFS = new CFK();
				cFFKAndCFS.FkId = f.FfkId;
				cFFKAndCFS.KindId = f.FirstKindId;
				cFFKAndCFS.KindName = f.FirstKindName;
				cFFKAndCFS.KindSalaryId = f.FirstKindSalaryId;
				cFFKAndCFS.KindSaleId = f.FirstKindSaleId;
				cFFKAndCFS.Children = new List<CFK>();
				foreach (CFSKs s in cFSKs)
				{
					if (f.FirstKindId == s.FirstKindId)
					{
						CFK cFFKAndCFS1 = new CFK();
						cFFKAndCFS1.FkId = s.FskId;
						cFFKAndCFS1.KindId = s.SecondKindId;
						cFFKAndCFS1.KindName = s.SecondKindName;
						cFFKAndCFS1.KindSalaryId = s.SecondSalaryId;
						cFFKAndCFS1.KindSaleId = s.SecondSaleId;
						cFFKAndCFS.Children.Add(cFFKAndCFS1);
					}
				}
				cFFKAndCFSKs.Add(cFFKAndCFS);
			}
			return Json(cFFKAndCFSKs);
		}

	}
}
