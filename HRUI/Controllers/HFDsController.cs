using EFCore;
using EFCore.Models;
using HRIServices;
using HRServices;
using HRUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text.Json;

namespace HRUI.Controllers
{
	/// <summary>
	/// 人力资源档案表
	/// </summary>
	public class HFDsController : Controller
	{
		private readonly ICMKsService cMKsService;
		private readonly ICMsService cMsService;
		private readonly ICFFKsService cFFKsService;
		private readonly ICFSKsService cFSKsService;
		private readonly ICFTKsService cFTKsService;
		private readonly IHFDsService cHFDsService;
		private readonly ISSsService sssService;
        private readonly IDistributedCache memoryCache;
        public HFDsController(ICMKsService cMKsService, ICMsService cCMsService,ICFFKsService cFFKsService,
			ICFSKsService cFSKsService, ICFTKsService cFTKsService,IHFDsService hFDsService, ISSsService sSsService, IDistributedCache memoryCache)
		{
			this.memoryCache = memoryCache;
			this.cMKsService = cMKsService;
			this.cMsService = cCMsService;
			this.cFTKsService = cFTKsService;
			this.cFFKsService = cFFKsService;
			this.cFSKsService = cFSKsService;
			this.cHFDsService = hFDsService;
			this.sssService = sSsService;
		}


		/// <summary>
		/// 跳转到调动登记的调动登记查询页面
		/// </summary>
		/// <returns></returns>
		public IActionResult Register_locate()
		{
			return View();
		}
		/// <summary>
		/// 根据传入的条件跳转到对应的页面
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Register_list(RegisterListTJDTO registerListTJDTO)
		{
			HFDs hFDs = new HFDs();
				
			if (registerListTJDTO.JsonSelected != null)
			{
				string[] a = JsonConvert.DeserializeObject<string[]>(registerListTJDTO.JsonSelected);
				hFDs.FirstKindId = a[0];
				hFDs.SecondKindId = a[1];
				hFDs.ThirdKindId = a[2];
			}

			List<HFDs> list = await cHFDsService.GetByRegistTimeAndHFDs(hFDs, registerListTJDTO.DateStar, registerListTJDTO.DateEnd);
			ViewData["list"] = System.Text.Json.JsonSerializer.Serialize(list);
			ViewData["tj"] = registerListTJDTO;
			return View();
		}

		public async Task<IActionResult> Register(string id)
		{
			try
			{
				HFDs hfds = await cHFDsService.GetByHumanIdAsync(id);
				return View(hfds);
			}catch (Exception ex)
			{
				throw new Exception(ex.ToString(), ex);
			}
			
		}


		/// <summary>
		/// 获取一二三级的级联控件
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> AllCMKsListGet()
		{
			List<CFK> cFFKAndCFSKs = new List<CFK>();
			// 查询一级
			List<CFFKs> cFFK = await cFFKsService.GetAllAsync();
			// 查询二级
			List<CFSKs> cFSK = await cFSKsService.GetAllAsync();
			//查询三级
			List<CFTKs> cFTK = await cFTKsService.GetAllAsync();
			// 循环合并
			foreach (CFFKs f in cFFK)
			{
				CFK cFFKAndCFS = new CFK();
				cFFKAndCFS.FkId = f.FfkId;
				cFFKAndCFS.KindId = f.FirstKindId;
				cFFKAndCFS.KindName = f.FirstKindName;
				cFFKAndCFS.KindSalaryId = f.FirstKindSalaryId;
				cFFKAndCFS.KindSaleId = f.FirstKindSaleId;
				cFFKAndCFS.Children = new List<CFK>();
				foreach (CFSKs s in cFSK)
				{
					if (f.FirstKindId == s.FirstKindId)
					{
						CFK cFFKAndCFS1 = new CFK();
						cFFKAndCFS1.FkId = s.FskId;
						cFFKAndCFS1.KindId = s.SecondKindId;
						cFFKAndCFS1.KindName = s.SecondKindName;
						cFFKAndCFS1.KindSalaryId = s.SecondSalaryId;
						cFFKAndCFS1.KindSaleId = s.SecondSaleId;
						cFFKAndCFS1.Children = new List<CFK>();
						cFFKAndCFS.Children.Add(cFFKAndCFS1);
						foreach (CFTKs t in cFTK)
						{
							if (s.SecondKindId == t.SecondKindId)
							{
								CFK cFFKAndCFS2 = new CFK();
								cFFKAndCFS2.FkId = t.FtkId;
								cFFKAndCFS2.KindId = t.ThirdKindId;
								cFFKAndCFS2.KindName = t.ThirdKindName;
								cFFKAndCFS2.KindSalaryId = t.ThirdKindId;
								cFFKAndCFS2.KindSaleId = t.ThirdKindSaleId;
								cFFKAndCFS1.Children.Add(cFFKAndCFS2);
							}
						}
					}
				}
				cFFKAndCFSKs.Add(cFFKAndCFS);
			}
			return Json(cFFKAndCFSKs);
		}
		/// <summary>
		/// 查询所有的薪酬标准
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GetSSs()
		{
			return Json(await sssService.GetAll());
		}
		/// <summary>
		/// 进行变更
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Register_success(HFDsJSBGDTO hFDsJSBGDTO)
		{
			string[] strings = JsonConvert.DeserializeObject<string[]>(hFDsJSBGDTO.JsonKindId);
			string[] hmks = JsonConvert.DeserializeObject<string[]>(hFDsJSBGDTO.JsonCMKAndCM);
			HFDs hFDs = new HFDs()
			{
				FirstKindId = strings[0],
				FirstKindName = (await cFFKsService.GetByFirstKindIdAsync(strings[0])).FirstKindName,
				SecondKindId = strings[1],
				SecondKindName = (await cFSKsService.GetBySecondKindIdAsync(strings[1])).SecondKindName,
				ThirdKindId = strings[2],
				ThirdKindName = (await cFTKsService.GetByThirdKindId(strings[2])).ThirdKindName,
				HumanMajorKindId = hmks[0],
				HumanMajorKindName = (await cMKsService.GetByMajorKindId(hmks[0])).MajorKindName,
				HumanMajorId = hmks[1],
				HunmaMajorName = (await cMsService.GetByMajorId(hmks[1], hmks[0])).MajorName,
				HumanId = hFDsJSBGDTO.HumanId,
				Remark = hFDsJSBGDTO.ChangeReason,
				MajorChangeAmount = (await cHFDsService.GetByHumanIdAsync(hFDsJSBGDTO.HumanId)).MajorChangeAmount,
				SalaryStandardId = hFDsJSBGDTO.sSelectedSSsId,
				SalaryStandardName = (await sssService.GetByStandardId(hFDsJSBGDTO.sSelectedSSsId)).StandardName,
				SalarySum = hFDsJSBGDTO.SalarySum,
			};
			if(await cHFDsService.UpdateHFDs(hFDs)){
				return View();
			}
			else
			{
				return View("Register_locate");
            }
        }
        public IActionResult Check_list()
        {
            return View();
        }

		public async Task<IActionResult> GetTableData()
        {
            //缓存键
            string did = "ddshList";

			string list = "";

            string ddshList = memoryCache.GetString(did);
            if (ddshList == null)
            {
                Console.WriteLine("从数据库查询");

                var listdata = await cHFDsService.GetHFsAllData(); // 访问数据库

				list = System.Text.Json.JsonSerializer.Serialize(listdata);

                DistributedCacheEntryOptions distributedCacheEntryOptions = new DistributedCacheEntryOptions();
                distributedCacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                memoryCache.SetString(did, list, distributedCacheEntryOptions);
            }
            else
            {
                Console.WriteLine("从缓存中查询");
				list = ddshList;
            }

            var a = System.Text.Json.JsonSerializer.Deserialize<List<HFs>>(list);
            return Json(a);
		}

		public async Task<IActionResult> Check(string id)
		{
			try
			{
				HFs hfds = await cHFDsService.GetByHumanIdOnHFsAsync(id);
				return View(hfds);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString(), ex);
			}
		}

		public async Task<IActionResult> Check_success(HFDsJSBGDTO hFDsJSBGDTO)
		{
			if (hFDsJSBGDTO.IsOk == "true")
			{
				string[] strings = JsonConvert.DeserializeObject<string[]>(hFDsJSBGDTO.JsonKindId);
				string[] hmks = JsonConvert.DeserializeObject<string[]>(hFDsJSBGDTO.JsonCMKAndCM);
				HFDs hFDs = new HFDs()
				{
					FirstKindId = strings[0],
					FirstKindName = (await cFFKsService.GetByFirstKindIdAsync(strings[0])).FirstKindName,
					SecondKindId = strings[1],
					SecondKindName = (await cFSKsService.GetBySecondKindIdAsync(strings[1])).SecondKindName,
					ThirdKindId = strings[2],
					ThirdKindName = (await cFTKsService.GetByThirdKindId(strings[2])).ThirdKindName,
					HumanMajorKindId = hmks[0],
					HumanMajorKindName = (await cMKsService.GetByMajorKindId(hmks[0])).MajorKindName,
					HumanMajorId = hmks[1],
					HunmaMajorName = (await cMsService.GetByMajorId(hmks[1], hmks[0])).MajorName,
					HumanId = hFDsJSBGDTO.HumanId,
					Remark = hFDsJSBGDTO.ChangeReason,
					MajorChangeAmount = (await cHFDsService.GetByHumanIdAsync(hFDsJSBGDTO.HumanId)).MajorChangeAmount + 1,
					SalaryStandardId = hFDsJSBGDTO.sSelectedSSsId,
					SalaryStandardName = (await sssService.GetByStandardId(hFDsJSBGDTO.sSelectedSSsId)).StandardName,
					SalarySum = hFDsJSBGDTO.SalarySum,
					CheckTime = Convert.ToDateTime(hFDsJSBGDTO.CheckTime),
					CheckStatus = 1,
					Checker = hFDsJSBGDTO.Checker,
				};
				if (await cHFDsService.IsUpdateHFDsTrue(hFDs))
				{
					await cHFDsService.DeleteHFsByHumanId(hFDsJSBGDTO.HumanId);
					return View();
				}
				else
				{
					return View("Check_list");
				}
			}
			else
			{
				await cHFDsService.DeleteHFsByHumanId(hFDsJSBGDTO.HumanId);
				return View("Check_list");
			}
		}

		public async Task<IActionResult> GetOldData(string id)
		{
			HFDs hfds = await cHFDsService.GetByHumanIdAsync(id);
		 	string jg = hfds.FirstKindName+"/"+hfds.SecondKindName+"/"+hfds.ThirdKindName;
			string xcmc = hfds.SalaryStandardName+"";
			string salarySum = hfds.SalarySum+"";
			string zwName = hfds.HumanMajorKindName+"";
			string zwfl = hfds.HunmaMajorName + "";
			string[] a = { jg, zwfl, zwName, xcmc, salarySum };
			return Json(a);
		}

		public IActionResult Locate()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ListPage([FromBody]LocateDTO locateDTO)
		{
			var sql = " And CheckStatus = 1";
			if (locateDTO != null)
			{
				if (locateDTO.JsonSelectedJG!=""&& locateDTO.JsonSelectedJG != null)
				{
					string[] strings = JsonConvert.DeserializeObject<string[]>(locateDTO.JsonSelectedJG);
					sql += $" And FirstKindId = '{strings[0]}' And SecondKindId = '{strings[1]}' And ThirdKindId = '{strings[2]}'";
				}
				if (locateDTO.JsonSelectedCmk!="" && locateDTO.JsonSelectedCmk != null)
				{
					string[] hmks = JsonConvert.DeserializeObject<string[]>(locateDTO.JsonSelectedCmk);
					sql += $" And HumanMajorKindId = '{hmks[0]}' And HumanMajorId = '{hmks[1]}'";
				}
				if (locateDTO.DateTimeStar!=null)
				{
					sql += $" And RegistTime >= '{locateDTO.DateTimeStar}'";
				}
				if (locateDTO.DateTimeEnd!=null)
				{
					sql += $" And RegistTime <= '{locateDTO.DateTimeEnd}'";
				}
			}
			List<HFDs> hFDs = await cHFDsService.GetByWhere(sql);
			ViewData["list"] = System.Text.Json.JsonSerializer.Serialize(hFDs);
			return View();
		}
	}
}
