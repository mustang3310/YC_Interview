using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YCInterview.Web.FileOperate;
using YCInterview.Web.Models;

namespace YCInterview.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : BaseController
    {
        private readonly IFileComponent fileComponent;

        public HomeController(
            IMapper mapper,
            IFileComponent fileComponent
            ) : base(mapper)
        {
            this.fileComponent = fileComponent;
        }

        /// <summary>
        /// 首頁
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            return View();
        }

        /// <summary>
        /// 關於我們
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> AboutUs()
        {
            return View();
        }

        /// <summary>
        /// 企業永續
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ESG()
        {
            return View();
        }

        /// <summary>
        /// 人才招募
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> JoinUs()
        {
            return View();
        }

        /// <summary>
        /// 聯絡我們
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ConnectionInfo()
        {
            return View();
        }

        /// <summary>
        /// 成功案例
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> SuccessCase()
        {
            return View();
        }

        /// <summary>
        /// 產品與解決方案
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Product()
        {
            return View();
        }

        /// <summary>
        /// 錯誤畫面
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult> Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
        }
    }
}