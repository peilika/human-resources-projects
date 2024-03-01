using EFCore.Models;
using HREF.Model;
using HRIServices;
using HRServices;
using HRUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;

namespace HRUI.Controllers
{
    public class SSDsController : Controller
    {
        private ISGService _sgService;
		private ISSDsService SSDS;
		private IUsersService UsersService;
		public SSDsController(ISGService  sGService,ISSDsService sSDsService,IUsersService usersService) 
        {
			this.UsersService=usersService;

		this._sgService = sGService;
			this.SSDS=sSDsService;
        }
        public IActionResult Register_locate()
        {
             return View();
        }
		public IActionResult Check_list()
		{
			return View();
		}
		public IActionResult query_locate()
        {
            return View();
        }
        //分页查询
        [HttpGet]
		public async Task<IActionResult> GetData(int pageSize, int pageIndex)
		{
			FenYeT<SG> fenYeT = new FenYeT<SG>()
			{
				PageIndex = pageIndex,
				PageSize = pageSize,
			};
			fenYeT = await _sgService.GetByFenYeByCheckStatusIsFalseAsync(fenYeT);
			return Json(fenYeT);
		}
		
		public IActionResult Register_list(string ff)
		{
			ViewData["id"] = ff;
			return View();
		}
		
		public async Task<ActionResult> SalaryPaymentInsertQuery2(string selectedValue3)
		{
			List<SG> model = new List<SG>();
			List<SG> mode2 = new List<SG>();
			if (string.IsNullOrEmpty(selectedValue3))
			{
				model = await SSDS.queryDJ();
				return Json(model);
			}
			else if (selectedValue3 == "first_kind_name")
			{

				model = await SSDS.queryDJ();
				foreach (var a in model)
				{


					if (!mode2.Any(m => m.FirstKindName == a.FirstKindName))
					{
						mode2.Add(a);
					}

				}

				return Json(mode2);
			}
			else if (selectedValue3 == "second_kind_name")
			{
				model = await SSDS.queryDJ();
				foreach (var a in model)
				{


					if (!mode2.Any(m => m.SecondKindName == a.SecondKindName))
					{
						mode2.Add(a);
					}

				}
				
				return Json(mode2);
			}
			else if (selectedValue3 == "third_kind_name")
			{

				model = await SSDS.queryDJ();
				foreach (var a in model)
				{


					if (!mode2.Any(m => m.ThirdKindName == a.ThirdKindName))
					{
						mode2.Add(a);
					}

				}

				return Json(mode2);


			}

			
			return View(model);
		}
		////测试
		//public async Task<IActionResult> aa(int id)
		//{
		//	HFDs hFDs = new HFDs();
		//	hFDs.HumanId = "nb123";
		//	hFDs.HumanName = "姜离";
		//	SG sG =new SG();
		//	sG.FirstKindId = "03";
		//	sG.FirstKindName = "傻逼北大青鸟";
		//	sG.SecondKindId = "04";
		//	sG.SecondKindName = ".net";
		//	sG.ThirdKindId = "01";
		//	sG.ThirdKindName = "李wue学习小组";
		//	sG.SalaryPaidSum = 1000;
		//	sG.SalaryGrantId = "op12345";
		//	sG.SalaryStandardSum = 2000;
		//	sG.HumanAmount = 1; ;
		//	int fir = await SSDS.Sgyz(sG,hFDs);

		//	return Json(fir);
		//}
		
		public async Task<IActionResult> ssd3query(int id)
		{
			SGD2 fir = await SSDS.QUERYsdg2(id);
			return Json(fir);
		}
		public async Task<IActionResult> ssd2query(string id)
		{
			List<SSDs> fir = await SSDS.QUERYSSDS(id);
			return Json(fir);
		}
		//记录登记人和登记时间
		public async Task<IActionResult> drigDJs(string date, string StandardId)
		{
			List<Users> a = await UsersService.GetAll();
			string v = a[0].UTrueName;

			int l = await SSDS.updateSG(v, date, StandardId,true); 
			return Json(l);

		}
		//记录登记人和登记时间
		public async Task<IActionResult> drigDJ(string date ,string StandardId) 
		{
		   List<Users> a =  await UsersService.GetAll();
			string v = a[0].UTrueName;
			int l = await SSDS.updateSG(v, date, StandardId);
			return Json(l);

		}

		public async Task<IActionResult> xgssg(int id ,int a,int b,int c,int d)
		{
			int dd = a + b - c;
			int dd2 = b + a;
			int ee = await SSDS.updateSSDS(id, a, b, c, dd,dd2);
			return Json(ee);
		}
		
		public async Task<IActionResult> Registerupda2(string id)
		{
			List<SGD2> fir = await SSDS.querySGD2(id);
			return Json(fir);
		}
		public async Task<IActionResult> Check(string salaryGrantId,string c)
		{
			ViewData["idd2"] = c;
			ViewData["idd"] = salaryGrantId;
			return View();
		}
		//查询薪资
		
    	public async Task<IActionResult> queylist(string id, string a, string b, string c)
		{
			List<SG> sGs = await SSDS.sG(id,a,b,c);
			
			return Json(sGs);
		}
		public async Task<IActionResult> query_click(string a,string b,string c,string d)
		{
			ViewData["id"] = a;
			ViewData["id2"] = b;
			ViewData["id3"] = c;
			ViewData["id4"] = d;
			return View();
		}

		public async Task<IActionResult> Registerupda(string id,string idd)
		{
			ViewData["idd"] = id;
			ViewData["idd2"] = idd;
		      return View();
		}
		string GenerateRandomDigits(int length)
		{
			Random random = new Random();
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < length; i++)
			{
				sb.Append(random.Next(10)); // 生成0-9之间的随机数字
			}

			return sb.ToString();
		}
		string GenerateCustomIdentifier()
		{
			string prefix = "HK";
			string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
			string randomDigits = GenerateRandomDigits(3); // 生成3位随机数字

			return prefix + timestamp + randomDigits;
		}
		//原神启动
		public async Task<IActionResult> query_listpro(string a, string b)
		{
			ViewData["id"] = a;
			ViewData["id2"] = b;
			return View();
		}



	}
}
