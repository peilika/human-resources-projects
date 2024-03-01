using EFCore.Models;
using HRIServices;
using HRServices;
using HRUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace HRUI.Controllers
{
    public class SSsController : Controller
    {
        private readonly ICPCsService ICPCsService;
        private readonly IUsersService IUsersService;
        private readonly ISSsService ISsService;
        private readonly ISSDsService ISDsService;

        public SSsController(ICPCsService ICPCsService,IUsersService IUsersService,ISSsService sSs, ISSDsService ISDsService)
        {
            this.ISDsService = ISDsService;
			this.ICPCsService = ICPCsService;
            this.IUsersService = IUsersService;
            this.ISsService = sSs;
        }
        /// <summary>
        /// 薪酬标准登记页展示
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Salarystandard_register()
        {
            SSs ss = new SSs();
            ss.StandardId = Guid.NewGuid().ToString();
            List<CPCs> a = await ICPCsService.GetByAttributeKind("薪酬设置");
            ViewData["cpcs"] = a;
            ViewData["users"] = await IUsersService.GetAll();
			return View(ss);
        }
        /// <summary>
        /// 根据传入的数据进行添加操作
        /// </summary>
        /// <param name="sSsAndSSDs"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Salarystandard_register_success([FromForm] SSsAndSSDs sSsAndSSDs)
         {
			var keyValuePairs = JsonConvert.DeserializeObject<List<JSONToSSDs>>(sSsAndSSDs.SelOptionsAllData);
            SSs sSs = new SSs()
            {
                SalarySum = sSsAndSSDs.SalarySum,
                Designer = sSsAndSSDs.Designer,
                StandardId = sSsAndSSDs.StandardId,
                StandardName = sSsAndSSDs.StandardName,
                Register = sSsAndSSDs.Register,
                Remark = sSsAndSSDs.Remark,
                RegistTime = sSsAndSSDs.RegistTime,
                ChangeStatus = 0,
                CheckStatus = 0,
			};
            List<SSDs> sSDses = new List<SSDs>(); 
            foreach (var a in keyValuePairs)
            {
				SSDs sSDs = new SSDs()
				{
					StandardId = sSsAndSSDs.StandardId,
					StandardName = sSsAndSSDs.StandardName,
					ItemId = Convert.ToInt32(a.PbcId),
					ItemName = a.AttributeName,
					Salary = Convert.ToDouble(a.Salary)
				};
                sSDses.Add(sSDs);
			}
            if(await ISsService.AddSSsAsync(sSs, sSDses))
            {
                return View();
            }
            else
            {
                return Content("系统故障");
            }
			
        }

		/// <summary>
		/// 薪酬标准登记复核展示
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Salarystandard_check_list()
        {
			return View();
        }
		/// <summary>
		/// 根据传入的薪酬标准单编号进入 薪酬标准登记复核 页面
		/// </summary>
		/// <param name="standardId"></param>
		/// <returns></returns>
		public async Task<IActionResult> Salarystandard_check(string standardId)
        {
            var a = await ISsService.GetByStandardId(standardId);
            a.CheckComment = WebUtility.HtmlDecode(a.CheckComment);

			ViewData["SSs"] = a; // 查询要修改的数据
            string b = WebUtility.HtmlDecode(JsonConvert.SerializeObject(await ISDsService.GetByStandardIdAsync(standardId)));
			ViewData["SSDs"] = b;   // 查询对应的详细信息
			return View();
        }
        /// <summary>
        /// 根据传入的数据进行复核操作
        /// </summary>
        /// <param name="scDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Salarystandard_check_success(SalarystandardCheckDTO scDTO)
        {
			List<SSDs> keyValuePairs = JsonConvert.DeserializeObject<List<SSDs>>(scDTO.JsonSSDs);
			SSs sSs = new SSs()
            {
				StandardId = scDTO.StandardId,
				StandardName = scDTO.StandardName,
				SalarySum = scDTO.SalarySum,
				Designer = scDTO.Designer,
				Checker = scDTO.Checker,
				CheckTime = scDTO.CheckTime,
				CheckComment = scDTO.CheckComment,
                CheckStatus = 1
			};
            // 进行修改
            if (await ISsService.UpdateSSsAsync(sSs, keyValuePairs))
            {
                return View();
            }
            else
            {
                return Content("unOk");
            }
			
        }

		/// <summary>
		/// 根据传入的数据进行分页查询
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <returns></returns>
		public async Task<IActionResult> GetData(int pageSize, int pageIndex)
        {
            FenYeT<SSs> fenYeT = new FenYeT<SSs>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            fenYeT = await ISsService.GetByFenYeByCheckStatusIsFalseAsync(fenYeT);
            return Json(fenYeT);
        }

        /// <summary>
        /// 打开薪酬标准查询页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Salarystandard_query_locate()
        {
            return View();
        }

		/// <summary>
		/// 根据传入的 薪酬标准编号 关键字 建档时间 分页地查询‘薪酬标准登记’表中的数据
		/// </summary>
		/// <returns></returns>
		[HttpPost]
        public async Task<IActionResult> Salarystandard_query_list([FromForm] SSsConditionDTO sSsConditionDTO)
        {
            string a = " And CheckStatus = 1 ";
            if (sSsConditionDTO.StandardName!="" && sSsConditionDTO.StandardName != null)
            {
                a += " And StandardName LIKE '%" + sSsConditionDTO.StandardName+"%'";
            }
            if (sSsConditionDTO.StandardId!="" && sSsConditionDTO.StandardId != null)
            {
                a += " And StandardId LIKE '%" + sSsConditionDTO.StandardId+"%'";
            }
            if (sSsConditionDTO.DateOne!="" && sSsConditionDTO.DateOne!=null)
            {
                a += " And RegistTime > '"+ sSsConditionDTO.DateOne + "'";  // 转型为DateTime
            }
            if (sSsConditionDTO.DateTwo != "" && sSsConditionDTO.DateTwo != null)
            {
                a += " And RegistTime < '"+ sSsConditionDTO.DateTwo + "'";  // 转型为DateTime
            }
            try
            {
                ViewData["list"] = System.Text.Json.JsonSerializer.Serialize(await GetByFenByWhereAsync(3, 1, a));
                ViewData["tj"] = a;
                return View();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
            
        }
        /// <summary>
        /// 根据传入的条件返回列表
        /// </summary>
        /// <param name="tj"></param>
        /// <returns></returns>
        public async Task<IActionResult> BackSalarystandard_query_list(string tj)
        {
			string replacedString = tj.Replace("\x01", "%");
			ViewData["list"] = System.Text.Json.JsonSerializer.Serialize(await GetByFenByWhereAsync(3, 1, replacedString));
			ViewData["tj"] = replacedString;
            return View("Salarystandard_query_list");
        }

		/// <summary>
		/// 根据传入的分页信息和条件进行查询
		/// </summary>
		/// <param name="pageSize"></param> 
		/// <param name="pageIndex"></param>
		/// <param name="tj"></param>
		/// <returns></returns>
		public async Task<IActionResult> FenYeByWhere(int pageSize, int pageIndex, string tj)
        {
			string replacedString = tj.Replace("\x01", "%");
			FenYeT<SSs> list = await GetByFenByWhereAsync(pageSize, pageIndex, replacedString);
            return Content(System.Text.Json.JsonSerializer.Serialize(list));
        }

		/// <summary>
		/// 根据分页和条件进行查询，返回分页对象
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="condition"></param>
		/// <returns></returns>
		public async Task<FenYeT<SSs>> GetByFenByWhereAsync(int pageSize, int pageIndex, string condition)
		{
            FenYeT<SSs> fenYeT = new FenYeT<SSs>()
            {
                PageIndex= pageIndex,
                PageSize = pageSize,
			};
			return await ISsService.GetByFenYeByWhereAsync(fenYeT, condition);
        }
        /// <summary>
        /// 跳转到修改页面
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Salarystandard_change(string id, string tj)
        {
			string replacedString = tj.Replace("\x01", "%");
			SSs sss = await ISsService.GetByStandardId(id);
            ViewData["SSDs"] = System.Text.Json.JsonSerializer.Serialize(await ISDsService.GetByStandardIdAsync(id));
            ViewData["tj"] = replacedString;
			return View(sss);
        }

        public async Task<IActionResult> Salarystandard_change_success(SSAndSSDDTO sSAndSSDDTO, string tj)
        {
			string replacedString = tj.Replace("\x01", "%");
			SSs sss = new SSs()
            {
                StandardId = sSAndSSDDTO.StandardId,
                StandardName = sSAndSSDDTO.StandardName,
                SalarySum =  sSAndSSDDTO.SalarySum,
                Remark = sSAndSSDDTO.Remark,
                Changer = sSAndSSDDTO.Changer,
                ChangeStatus = 1,
                ChangeTime = sSAndSSDDTO.ChangeTime,
            };
			List<SSDs> sSDs = JsonConvert.DeserializeObject<List<SSDs>>(sSAndSSDDTO.JsonSSDs);
            if (await ISsService.UpdateSSsAsync(sss, sSDs))
            {
                // 成功
				ViewData["tj"] = replacedString;
                return View();
            }
            else
            {
				// 失败
				ViewData["tj"] = replacedString;
                ViewData["isok"] = "unok";
                return View("Salarystandard_change", await ISsService.GetByStandardId(sss.StandardId));
			}
		}
        /// <summary>
        /// 跳转到查询页面
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Salarystandard_query(string standardId, string tj)
        {
			string replacedString = tj.Replace("\x01", "%");
			ViewData["ssds"] = System.Text.Json.JsonSerializer.Serialize(await ISDsService.GetByStandardIdAsync(standardId));
            ViewData["tj"] = replacedString;
            return View(await ISsService.GetByStandardId(standardId));
        }
        /// <summary>
        /// 根据传入的id查询薪酬总额
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSumById(string id)
        {
            return Content(""+(await ISsService.GetByStandardId(id)).SalarySum);
        }
	}
}
