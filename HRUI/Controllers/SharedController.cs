using HRIServices;
using Microsoft.AspNetCore.Mvc;

namespace HRUI.Controllers
{
	public class SharedController : Controller
	{
		private readonly IPowersService powersService;
		private readonly IPowersService powersServices;

		public SharedController(IPowersService powersService, IPowersService powersServices)
		{
			this.powersService = powersService;
			this.powersServices = powersServices;
		}
		private async Task<int> GetRolesID(int uid)
		{
			int rID = await powersService.GetUid(uid);
			return rID;
		}
		public async Task<List<int>> Left(int uid)
		{
			int rid = GetRolesID(uid).Result;
			List<int> jlist = await powersService.GetGid2(rid);
			return jlist;
		}
	}
}
