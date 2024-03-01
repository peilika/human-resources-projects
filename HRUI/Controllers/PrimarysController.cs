using Microsoft.AspNetCore.Mvc;

namespace HRUI.Controllers
{
	public class PrimarysController:Controller
	{
		public async Task<ActionResult> Primary_key()
		{
			return View();
		}
		//跳转开始
		public async Task<ActionResult> Primary_key_Register()
		{
			return View();
		}
	}
}
