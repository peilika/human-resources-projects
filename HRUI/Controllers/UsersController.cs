using EFCore.Models;
using HRIServices;
using Microsoft.AspNetCore.Mvc;

namespace HRUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        /// <summary>
        /// 用于测试前台页面与Controller的连接
        /// </summary>
        /// <returns></returns>
        public IActionResult Test() { return Content("测试成功"); }

        /// <summary>
        /// 用于进入登陆页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用于验证登录
        /// </summary>
        /// <returns></returns>
        [Route("Users/LoginAsync")]
        public async Task<IActionResult> LoginAsync(string uName, string uPassWord)
        {
            try
            {
                Users user = new Users() { UName = uName, UPassWord = uPassWord };
                user = await usersService.LoginAsync(user);
                if (user!=null)
                {
				    return Json(user);
                }
                else
                {
                    return Content("");
                }
            }catch (Exception ex)
            {
                throw new Exception();
            }
            
        }
    }
}
