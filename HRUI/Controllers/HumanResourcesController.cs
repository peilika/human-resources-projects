using EFCore.Models;
using HRIServices;
using HRServices;
using HRUI.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.IO;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.Net.Mail;
using System.Threading.Channels;
using System;

namespace HRUI.Controllers
{
	public class HumanResourcesController:Controller
	{
		private readonly IHumanResourcesService humanResources;
		private readonly IHumanResourcesService humanResourcess;
		private readonly ICFFKsService cFFKs;
		private readonly ICFSKsService cFSKs;
		private readonly ICFTKsService cFTKs;
		private readonly ICPCsService cPC;
		private readonly ICPCsService cPCs;
		private readonly ICPCsService cPCss;
		private readonly ICPCsService cPCsss;
		private readonly ICPCsService cPCssss;
		private readonly ICPCsService cPCsssss;
		private readonly ICMKsService cMKsService;
		private readonly ICMsService cMsService;
		private readonly ISSDsService ssdService;
		private readonly ISSsService sssService;
		private readonly ISSsService ssssService;
		private readonly IWebHostEnvironment _environment;
		private readonly ISSDsService SSSD;
		public HumanResourcesController(IHumanResourcesService humanResources, ICFFKsService cFFKs, ICFSKsService cFSKs, ICFTKsService cFTKs, ICPCsService cPC, ICPCsService cPCs,ICPCsService cPCss,
			ICMKsService cMKsService, ICMsService cMsService, ISSDsService ssdService, ISSsService sssService, IWebHostEnvironment environment, IHumanResourcesService humanResourcess, ICPCsService cPCsss, ICPCsService cPCssss, ICPCsService cPCsssss,ISSsService ssssService, ISSDsService sSDsService)
		{
			this.SSSD = sSDsService;
			this.humanResources = humanResources;
			this.cFFKs = cFFKs;
			this.cFSKs = cFSKs;
			this.cFTKs = cFTKs;
			this.cPC = cPC;
			this.cPCs = cPCs;
			this.cPCss = cPCss;
			this.cMKsService = cMKsService;
			this.cMsService = cMsService;
			this.ssdService = ssdService;
			this.sssService = sssService;
			_environment = environment;
			this.humanResourcess = humanResourcess;
			this.cPCsss = cPCsss;
			this.cPCssss = cPCssss;
			this.cPCsssss = cPCsssss;
			this.ssssService = ssssService;
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
		//查询
		public async Task<ActionResult> Check_list(FenYePage page)
		{
			if (page.PageSize == 0 && page.PageIndex == 0)
			{
				// 如果 page 参数为 null，则设置默认值
				page = new FenYePage { PageIndex = 1, PageSize = 5 };
			}
			List<HFDs> list = await humanResources.Check_list(page);
			ViewData["Page"] = page;
			ViewData["CheckList"] = list;
			ViewData["CheckCount"] = Check_ListCount().Result;
			return View();
		}
        private async Task<int> Check_ListCount()
        {
            int count = await humanResources.Check_ListCount();
            return count;
        }
        [HttpGet]
		public async Task<ActionResult> Human_Register(int id)
		{
			ERs ers = await humanResources.List_ER(id);
			CPCs cpcGJ = await cPCs.Public_chars(ers.HumanNationality);
			CPCs cpcMZ = await cPCss.Public_charMZ(ers.HumanRace);
			CPCs cpcXL = await cPCss.Public_chars(ers.HumanEducatedDegree);
			CPCs cpcNX = await cPCsss.Public_charMZ(ers.HumanEducatedYears);
			CPCs cpcZY = await cPCsss.Public_chars(ers.HumanEducatedMajor);
			CPCs cpcAH = await cPCssss.Public_charMZ(ers.HumanHobby);
			CPCs cpcTC = await cPCssss.Public_chars(ers.HumanSpeciality);
			CPCs cpcXY = await cPCsssss.Public_charMZ(ers.HumanReligion);
			CPCs cpcMM = await cPCsssss.Public_chars(ers.HumanParty);
			SSs ssdXC = await ssssService.GetName(ers.DemandSalaryStandard);

			ViewData["GJ"] = cpcGJ;
			ViewData["MZ"] = cpcMZ;
			ViewData["XL"] = cpcXL;
			ViewData["NX"] = cpcNX;
			ViewData["ZY"] = cpcZY;
			ViewData["AH"] = cpcAH;
			ViewData["TC"] = cpcTC;
			ViewData["XY"] = cpcXY;
			ViewData["MM"] = cpcMM;
			ViewData["XC"] = ssdXC;

			ViewData["listER"] = ers;
			ViewData["list1"] = await cFFKs.GetAllAsync();
			ViewData["list2"] = await cFSKs.GetAllAsync();
			ViewData["list3"] = await cFTKs.GetAllAsync();
			ViewData["list4"] = await cPC.Public_char();
			return View();
		}
		// 机构级联
		public async Task<IActionResult> GetCFFKsAndCFSKs()
		{
			List<CFK> cFFKAndCFSKs = new List<CFK>();
			// 查询一级
			List<CFFKs> cFFK = await cFFKs.GetAllAsync();
			// 查询二级
			List<CFSKs> cFSK = await cFSKs.GetAllAsync();
			//查询三级
			List<CFTKs> cFTK = await cFTKs.GetAllAsync();
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
						foreach(CFTKs t in cFTK)
						{
							if(s.SecondKindId == t.SecondKindId)
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
		// 获取职位的级联
		public async Task<IActionResult> GetCMKs()
		{
			List<CMAndCMKDTO> cMAndCMKs = new List<CMAndCMKDTO>();
			List<CMKs> cMKs = await cMKsService.GetAllAsync();
			List<CMs> cMs = await cMsService.GetAllAsync();
			foreach (var cmk in cMKs)
			{
				CMAndCMKDTO cmkDTO = new CMAndCMKDTO();
				cmkDTO.Id = cmk.MfkId;
				cmkDTO.MajorKindId = cmk.MajorKindId;
				cmkDTO.MajorKindName = cmk.MajorKindName;
				cmkDTO.Children = new List<CMAndCMKDTO>();
				foreach (var cms in cMs)
				{
					if (cmk.MajorKindId == cms.MajorKindId)
					{
						CMAndCMKDTO cmsDTO = new CMAndCMKDTO();
						cmsDTO.Id = cms.MakId;
						cmsDTO.MajorKindId = cms.MajorId;
						cmsDTO.MajorKindName = cms.MajorName;
						cmkDTO.Children.Add(cmsDTO);
					}
				}
				cMAndCMKs.Add(cmkDTO);
			}
			return Json(cMAndCMKs);
		}
		// 根据传入的类型字段获取对应的信息集合
		public async Task<IActionResult> GetByAttributeKind(string type)
		{
			return Json(await cPCs.GetByAttributeKind(type));
		}
		// 获取所有的薪酬标准信息
		public async Task<IActionResult> GetSSs()
		{
			var a = await sssService.GetAll();
			return Json(a);
		}
		// 处理图片上传
		[HttpPost]
		public IActionResult ImageUpload(IFormFileCollection file)
		{
			string filePath = "";
			if (file.Count > 0)
			{
				foreach (var item in file)
				{
					//生成文件名
					string name = DateTime.Now.ToString("yyyyMMddHHmmssfff");
					//获取后缀名
					string ext = item.FileName.Substring(item.FileName.LastIndexOf('.'));
					//设置上传路径
					string path = $"Uploader/{DateTime.Now.ToString("yyyy/MM/dd/")}" + name + ext;
					//Core生成文件的绝对路径
					var uploads = Path.Combine(_environment.WebRootPath, path);
					//创建上传文件对应的文件夹
					(new FileInfo(uploads)).Directory.Create();
					using (var stream = new FileStream(uploads, FileMode.CreateNew))
					{
						item.CopyTo(stream);//图片上传
											//TempData["name"] = name + ext;//保存图片的值
					}
					filePath = path;
				}
				return Content("/"+filePath);
			}
			else
			{
				return Content("添加失败");
			}
		}
		[HttpPost]
		public async Task<IActionResult> InsertHFDs(HFDDTO hFDDTO)
		{
			List<string> keyValuePairs = JsonConvert.DeserializeObject<List<string>>(hFDDTO.JsonSelected);  // 三级级联
			List<string> keyValuePairs1 = JsonConvert.DeserializeObject<List<string>>(hFDDTO.JsonSelectedCMKs); // 职位分类，职位设置

			List<CFTKs> cftk = await cFTKs.GetCFSKsByFFKIdAsync(keyValuePairs[0], keyValuePairs[1], keyValuePairs[2]);

			List<CMs> CMs = await cMsService.GetAsync(keyValuePairs1[0], keyValuePairs1[1]);

			SSs ss = await ssssService.GetID(hFDDTO.SalaryStandardId);

			int[] a = new int[]
			{
				hFDDTO.HumanNationality,
				hFDDTO.HumanRace,
				hFDDTO.HumanReligion,
				hFDDTO.HumanParty,
				hFDDTO.HumanEducatedDegree,
				hFDDTO.HumanEducatedYears,
				hFDDTO.HumanSpeciality,
				hFDDTO.HumanHobby,
				hFDDTO.SelectedCpc,
				hFDDTO.HumanEducatedMajor
			};
			List<CPCs> cpcs = await cPCs.GetByPbcIds(a);
			HFDs hFDs = new HFDs()
			{
				HumanId = Guid.NewGuid().ToString(),
				FirstKindId = cftk[0].FirstKindId,
				FirstKindName = cftk[0].FirstKindName,
				SecondKindId = cftk[0].SecondKindId,
				SecondKindName = cftk[0].SecondKindName,
				ThirdKindId = cftk[0].ThirdKindId,
				ThirdKindName = cftk[0].ThirdKindName,
				HumanMajorKindId = CMs[0].MajorKindId,
				HumanMajorKindName = CMs[0].MajorKindName,
				HumanMajorId = CMs[0].MajorId,
				HunmaMajorName = CMs[0].MajorName,
				HumanProDesignation = cpcs[8].AttributeName,
				HumanName = hFDDTO.HumanName,
				HumanSex = hFDDTO.HumanSex,
				HumanAddress = hFDDTO.HumanAddress,
				HumanPostcode = hFDDTO.HumanPostcode,
				HumanNationality = cpcs[0].AttributeName,
				HumanBirthplace = hFDDTO.HumanBirthplace,
				HumanBirthday = hFDDTO.HumanBirthday,
				HumanRace = cpcs[1].AttributeName,
				HumanReligion = cpcs[2].AttributeName,
				HumanParty = cpcs[3].AttributeName,
				HumanIdCard = hFDDTO.HumanIdCard,
				HumanSocietySecurityId = hFDDTO.HumanSocietySecurityId,
				HumanAge = hFDDTO.HumanAge,
				HumanEducatedDegree = cpcs[4].AttributeName,
				HumanEducatedYears = cpcs[5].AttributeName,
				HumanEducatedMajor = cpcs[9].AttributeName,
				SalaryStandardId = ss.StandardId,
				SalaryStandardName = ss.StandardName,
				HumanSpeciality = cpcs[6].AttributeName,
				HumanHobby = cpcs[7].AttributeName,
				HumanHistroyRecords = hFDDTO.HumanHistroyRecords,
				HumanFamilyMembership = hFDDTO.HumanFamilyMembership,
				Remark = hFDDTO.Remark,
				HumanPicture = hFDDTO.ImageUrl,
				HumanBank = hFDDTO.HumanBank,
				HumanAccount = hFDDTO.HumanAccount,
				HumanQQ = hFDDTO.HumanQQ,
				HumanTelephone = hFDDTO.HumanTelephone,
				HumanMobilephone = hFDDTO.HumanMobilephone,
				HumanEmail = hFDDTO.HumanEmail,
				Register = hFDDTO.Register,
				RegistTime = hFDDTO.RegistTime,
				SalarySum = ss.SalarySum
			};
			//薪资发放做验证数据
			SG sG = new SG();
			sG.FirstKindId = hFDs.FirstKindId;
			sG.FirstKindName = hFDs.FirstKindName;
			sG.SecondKindId = hFDs.SecondKindId;
			sG.SecondKindName = hFDs.SecondKindName;
			sG.ThirdKindId = hFDs.ThirdKindId;
			sG.ThirdKindName = hFDs.ThirdKindName;
			sG.HumanAmount = 1;
			sG.SalaryStandardId = hFDs.SalaryStandardId;
			
			sG.SalaryPaidSum = (int?)hFDs.PaidSalarySum;
			sG.SalaryStandardSum = (int?)hFDs.SalarySum;
			sG.SalaryGrantId = "op" + GetRandomNumber(10000, 99999).ToString();
			SSSD.Sgyz(sG,hFDs);
			bool zhi = await humanResources.HFDsInsert(hFDs);
			if (zhi)
			{
				FenYePage page = new FenYePage()
				{
					PageIndex = 1,
					PageSize = 5
				};

				List<HFDs> list = await humanResources.Check_list(page);
				ViewData["Page"] = page;
				ViewData["CheckList"] = list;
				ViewData["CheckCount"] = list.Count;

				return View("Check_list");
			}
			throw new Exception();
		}
		public async Task<ActionResult> Query_locate()
		{
			ViewData["list1"] = await cFFKs.GetAllAsync();
			ViewData["list2"] = await cFSKs.GetAllAsync();
			ViewData["list3"] = await cFTKs.GetAllAsync();
			ViewData["list4"] = await cPCs.Public_char();
			ViewData["list5"] = await humanResources.SelectCMs();
			ViewData["list6"] = await humanResources.Select();
			return View();
		}
		// 随机生成指定范围内的数字
		int GetRandomNumber(int min, int max)
		{
			Random random = new Random();
			return random.Next(min, max);
		}
		//人力资源复核
		[HttpGet]
		public async Task<ActionResult> Query_list_information(int id)
		{
			HFDs hf = await humanResources.QueryLocate(id);
			ViewData["list"] = hf;
			ViewData["list4"] = await cPCs.Public_char();
			return View();
		}
		[HttpPost]
		public async Task<ActionResult> Query_list_information(int id,int id2,string name,DateTime time,FenYePage page)
		{
			var zhi = await humanResources.Human_Update(id,id2,name,time);
			if (zhi)
			{
				if (page.PageSize == 0 && page.PageIndex == 0)
				{
					// 如果 page 参数为 null，则设置默认值
					page = new FenYePage { PageIndex = 1, PageSize = 5 };
				}
				List<HFDs> list = await humanResources.Check_list(page);
				ViewData["Page"] = page;
				ViewData["CheckList"] = list;
				ViewData["CheckCount"] = list.Count;
				return View("Check_List");
			}
			else
			{
				return await Query_list_information(id, id2, name, time,page);
			}
		}
		public async Task<ActionResult> Change_list(CFTKs cftk, CMs cms, DateTimeModel dt)
		{
			FenYeT<HFDs> page = new FenYeT<HFDs>()
			{
				PageIndex = 1,
				PageSize = 5
			};
			var selectStr = "";
			if (cftk.FirstKindName != "全部" && cftk.FirstKindName != null)
			{
				selectStr += " And FirstKindName ='" + cftk.FirstKindName + "'";
			}
			if (cftk.SecondKindName != "全部" && cftk.SecondKindName != null)
			{
				selectStr += " And SecondKindName ='" + cftk.SecondKindName + "'";
			}
			if (cftk.ThirdKindName != "全部" && cftk.ThirdKindName != null)
			{
				selectStr += " And ThirdKindName ='" + cftk.ThirdKindName + "'";
			}
			if (cms.MajorKindName != "全部" && cms.MajorKindName != null)
			{
				selectStr += " And HumanMajorKindName ='" + cms.MajorKindName + "'";
			}
			if (cms.MajorName != "全部" && cms.MajorName != null)
			{
				selectStr += " And HunmaMajorName ='" + cms.MajorName + "'";
			}
			try
			{
				selectStr += " And CheckStatus = 1";
				if (dt.datetime1 != null)
				{
					selectStr += " And RegistTime >'" + dt.datetime1 + "'";
				}
				if (dt.datetime2 != null)
				{
					selectStr += " And RegistTime <'" + dt.datetime2 + "'";
				}
				List<HFDs> hfds = await humanResourcess.SelectQuery(page, selectStr);
				if (hfds.Count > 0 || hfds != null)
				{
					ViewData["page"] = page;
					ViewData["list"] = hfds;
					ViewData["Count"] = hfds.Count;
					return View("Change_list");
				}
				else
				{
					TempData["Message"] = "未查询出满足条件的数据";
					return View("Query_locate");
				}
			}catch(Exception ex)
			{
				throw new Exception();
			}
		}
		public async Task<IActionResult> GetCMKss()
		{
			List<CMAndCMKDTO> cMAndCMKs = new List<CMAndCMKDTO>();
			List<CMKs> cMKs = await cMKsService.GetAllAsync();
			List<CMs> cMs = await cMsService.GetAllAsync();
			cMAndCMKs.Add(new CMAndCMKDTO() { MajorKindId = "0", MajorKindName = "全部" });
			foreach (var cmk in cMKs)
			{
				CMAndCMKDTO cmkDTO = new CMAndCMKDTO();
				cmkDTO.Id = cmk.MfkId;
				cmkDTO.MajorKindId = cmk.MajorKindId;
				cmkDTO.MajorKindName = cmk.MajorKindName;
				cmkDTO.Children = new List<CMAndCMKDTO>();
				cmkDTO.Children.Add(new CMAndCMKDTO() { MajorKindId = "0", MajorKindName = "全部" });
				foreach (var cms in cMs)
				{
					if (cmk.MajorKindId == cms.MajorKindId)
					{
						CMAndCMKDTO cmsDTO = new CMAndCMKDTO();
						cmsDTO.Id = cms.MakId;
						cmsDTO.MajorKindId = cms.MajorId;
						cmsDTO.MajorKindName = cms.MajorName;
						cmkDTO.Children.Add(cmsDTO);
					}
				}
				cMAndCMKs.Add(cmkDTO);
			}
			return Json(cMAndCMKs);
		}
		public async Task<IActionResult> GetCFFKsAndCFSKss()//带全部的级联
		{
			List<CFK> cFFKAndCFSKs = new List<CFK>();
			// 查询一级
			List<CFFKs> cFFK = await cFFKs.GetAllAsync();
			// 查询二级
			List<CFSKs> cFSK = await cFSKs.GetAllAsync();
			//查询三级
			List<CFTKs> cFTK = await cFTKs.GetAllAsync();
			cFFKAndCFSKs.Add(new CFK() { KindId = "0", KindName = "全部" });
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
				cFFKAndCFS.Children.Add(new CFK() { KindId = "0", KindName = "全部" });
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
						cFFKAndCFS1.Children.Add(new CFK() { KindId = "0", KindName = "全部" });
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
		[HttpGet]
		public async Task<ActionResult> Change_list_information(int id)
		{
			HFDs hf = await humanResourcess.QueryLocate(id);
			ViewData["list"] = hf;
			ViewData["list4"] = await cPCs.Public_char();
			return View();
		}
		[HttpPost]
		public async Task<ActionResult> Change_list_information(HFDsDAO hfdsdto)
		{
			SSs s = await sssService.GetSID(hfdsdto.SalaryStandardId);
			HFDs hf = new HFDs()
			{
				HfdId = hfdsdto.HfdId,
				HumanPicture = hfdsdto.imageUrl,
				HumanName = hfdsdto.HumanName,
				HumanAddress = hfdsdto.HumanAddress,
				HumanPostcode = hfdsdto.HumanPostcode,
				HumanTelephone = hfdsdto.HumanTelephone,
				HumanMobilephone = hfdsdto.HumanMobilephone,
				HumanBank = hfdsdto.HumanBank,
				HumanAccount = hfdsdto.HumanAccount,
				HumanQQ = hfdsdto.HumanQQ,
				HumanEmail = hfdsdto.HumanEmail,
				HumanSex = hfdsdto.HumanSex,
				HumanBirthday = hfdsdto.HumanBirthday,
				HumanBirthplace = hfdsdto.HumanBirthplace,
				HumanAge = hfdsdto.HumanAge,
				HumanSocietySecurityId = hfdsdto.HumanSocietySecurityId,
				HumanIdCard = hfdsdto.HumanIdCard,
				Remark = hfdsdto.Remark,
				SalaryStandardId = s.StandardId,
				SalaryStandardName = s.StandardName,
				HumanFamilyMembership = hfdsdto.HumanFamilyMembership,
				Changer = hfdsdto.Changer,
				ChangeTime = hfdsdto.ChangeTime,
				LastlyChangeTime = hfdsdto.LastlyChangeTime,
				HumanNationality = hfdsdto.HumanNationality,
				HumanRace = hfdsdto.HumanRace,
				HumanReligion = hfdsdto.HumanReligion,
				HumanParty = hfdsdto.HumanParty,
				HumanEducatedDegree = hfdsdto.HumanEducatedDegree,
				HumanEducatedYears = hfdsdto.HumanEducatedYears,
				HumanEducatedMajor = hfdsdto.HumanEducatedMajor,
				HumanSpeciality = hfdsdto.HumanSpeciality,
				HumanHobby = hfdsdto.HumanHobby,
				HumanHistroyRecords = hfdsdto.HumanHistroyRecords,
				HumanId = hfdsdto.HumanId,
				FirstKindId = hfdsdto.FirstKindId,
				FirstKindName = hfdsdto.FirstKindName,
				SecondKindId = hfdsdto.SecondKindId,
				SecondKindName = hfdsdto.SecondKindName,
				ThirdKindId = hfdsdto.ThirdKindId,
				ThirdKindName = hfdsdto.ThirdKindName,
				HumanProDesignation = hfdsdto.HumanProDesignation,
				HumanMajorKindId = hfdsdto.HumanMajorKindId,
				HumanMajorKindName = hfdsdto.HumanMajorKindName,
				HumanMajorId = hfdsdto.HumanMajorId,
				HunmaMajorName = hfdsdto.HunmaMajorName,
				SalarySum = hfdsdto.SalarySum,
				DemandSalaraySum = hfdsdto.DemandSalaraySum,
				PaidSalarySum = hfdsdto.PaidSalarySum,
				MajorChangeAmount = hfdsdto.MajorChangeAmount,
				BonusAmount = hfdsdto.BonusAmount,
				TrainingAmount = hfdsdto.TrainingAmount,
				FileChangAmount = hfdsdto.FileChangAmount,
				AttachmentName = hfdsdto.AttachmentName,
				CheckStatus = hfdsdto.CheckStatus,
				Register = hfdsdto.Register,
				Checker = hfdsdto.Checker,
				RegistTime = hfdsdto.RegistTime,
				CheckTime = hfdsdto.CheckTime,
				DeleteTime = hfdsdto.DeleteTime,
				RecoveryTime = hfdsdto.RecoveryTime,
				HumanFileStatus = hfdsdto.HumanFileStatus,

			};
			bool zhi = await humanResources.HFDsUpdate(hf);
			if(zhi)
			{
				FenYeT<HFDs> page = new FenYeT<HFDs>()
				{
					PageIndex = 1,
					PageSize = 5
				};
				var selectStr = "";
				selectStr += " And CheckStatus =" + 1;
				List<HFDs> hfds = await humanResourcess.SelectQuery(page, selectStr);
				if (hfds.Count > 0 || hfds != null)
				{
					ViewData["page"] = page;
					ViewData["list"] = hfds;
					return View("Change_list");
				}
				return View("Change_list");
			}
			else
			{
				return View();
			}
		}
		//标记删除
		public async Task<ActionResult> Delete_list(FenYePage page)
		{
			if (page.PageSize == 0 && page.PageIndex == 0)
			{
				// 如果 page 参数为 null，则设置默认值
				page = new FenYePage { PageIndex = 1, PageSize = 5 };
			}

			List<HFDs> list = await humanResources.Delete_list(page);
			ViewData["Page"] = page;
			ViewData["DelectSelect"] = list;
			ViewData["DeleteCount"] = list.Count;

			return View();
		}
		public async Task<ActionResult> Deletion(int id,FenYePage page)
		{
			bool zhi = await humanResources.Deletion(id);
			if(zhi)
			{
				if (page.PageSize == 0 && page.PageIndex == 0)
				{
					// 如果 page 参数为 null，则设置默认值
					page = new FenYePage { PageIndex = 1, PageSize = 5 };
				}

				List<HFDs> list = await humanResources.Delete_list(page);
				ViewData["Page"] = page;
				ViewData["DelectSelect"] = list;
				ViewData["DeleteCount"] = list.Count;
				ViewData["Delete"] = "已标记删除";

				return View("Delete_list");
			}
			return View();
		}
		public async Task<ActionResult> Recover(int id, FenYePage page)
		{
			bool zhi = await humanResources.Recover(id);
			if (zhi)
			{
				if (page.PageSize == 0 && page.PageIndex == 0)
				{
					// 如果 page 参数为 null，则设置默认值
					page = new FenYePage { PageIndex = 1, PageSize = 5 };
				}

				List<HFDs> list = await humanResources.Delete_list(page);
				ViewData["Page"] = page;
				ViewData["DelectSelect"] = list;
				ViewData["DeleteCount"] = list.Count;
				ViewData["Recover"] = "已恢复";

				return View("Delete_list");
			}
			return View();
		}

		//恢复
		public async Task<ActionResult> Recovery_list(FenYePage page)
		{
			if (page.PageSize == 0 && page.PageIndex == 0)
			{
				// 如果 page 参数为 null，则设置默认值
				page = new FenYePage { PageIndex = 1, PageSize = 5 };
			}

			List<HFDs> list = await humanResources.Recovery_list(page);
			ViewData["Page"] = page;
			ViewData["RecoverSelect"] = list;
			ViewData["RecoverCount"] = list.Count;

			return View();
		}
		public async Task<ActionResult> Recovery(int id, FenYePage page)
		{
			bool zhi = await humanResources.Recover(id);
			if (zhi)
			{
				if (page.PageSize == 0 && page.PageIndex == 0)
				{
					// 如果 page 参数为 null，则设置默认值
					page = new FenYePage { PageIndex = 1, PageSize = 5 };
				}

				List<HFDs> list = await humanResources.Recovery_list(page);
				ViewData["Page"] = page;
				ViewData["RecoverSelect"] = list;
				ViewData["RecoverCount"] = list.Count;
				ViewData["Recovery"] = "已恢复";

				return View("Recovery_list");
			}
			return View();
		}

		//用具删除
		public async Task<ActionResult> Delete_forever_list(FenYePage page)
		{
			if (page.PageSize == 0 && page.PageIndex == 0)
			{
				// 如果 page 参数为 null，则设置默认值
				page = new FenYePage { PageIndex = 1, PageSize = 5 };
			}

			List<HFDs> list = await humanResources.Delete_forever_list(page);
			ViewData["Page"] = page;
			ViewData["DelectSelect"] = list;
			ViewData["DeleteCount"] = list.Count;

			return View();
		}
		public async Task<ActionResult> ForeverDelete(int id,FenYePage page)
		{
			bool zhi = await humanResources.ForeverDelete(id);
			if (zhi)
			{
				if (page.PageSize == 0 && page.PageIndex == 0)
				{
					// 如果 page 参数为 null，则设置默认值
					page = new FenYePage { PageIndex = 1, PageSize = 5 };
				}

				List<HFDs> list = await humanResources.Delete_forever_list(page);
				ViewData["Page"] = page;
				ViewData["DelectSelect"] = list;
				ViewData["DeleteCount"] = list.Count;
				ViewData["ForeverDelete"] = "删除成功";

				return View("Delete_forever_list");
			}
			return View();
		}
	}
}
