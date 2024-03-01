using Microsoft.AspNetCore.Mvc;

namespace HRUI.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
