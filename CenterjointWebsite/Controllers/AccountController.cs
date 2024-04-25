namespace YCInterview.Web.Controllers
{
    using Application.Interfaces;
    using AutoMapper;
    using Domain.Models;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using OfficialWebsite.Core;
    using OfficialWebsite.Core.Model;
    using OfficialWebsite.Core.Services;
    using System.Security.Claims;
    using YCInterview.Web.Validators;
    using YCInterview.Web.ViewModels;

    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;
        private readonly PasswordValidator passwordValidator;
        private readonly string recaptcha = nameof(recaptcha);
        private readonly RecaptchaModel recaptchaModel;
        private readonly ILoginLogService loginLogService;

        public AccountController(IMapper mapper
            , IAccountService accountService
            , PasswordValidator validator
            , ConfigurationService configurationService
            , ILoginLogService loginLogService
            ) : base(mapper)
        {
            this.accountService = accountService;
            this.passwordValidator = validator;
            this.loginLogService = loginLogService;

            // 將 appsetting 的驗證碼設定初始化
            recaptchaModel = configurationService.GetModel<RecaptchaModel>(ConfigurationOptions.Recaptcha);
        }

        #region 登入

        /// <summary>
        /// 登入畫面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (this.User.Identity is not null && this.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Recaptcha = recaptchaModel;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            ViewBag.Recaptcha = recaptchaModel;
            LoginLogModel loginLogModel = mapper.Map<LoginLogModel>(model);

#if RELEASE
            RecaptchaResultModel verifyRecaptcha = CheckRecaptcha(model.Token);
            // 檢查驗證碼
            if (!verifyRecaptcha.success)
            {
                verifyRecaptcha.ErrorMessage.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(nameof(verifyRecaptcha.ErrorMessage), error);
                });
            }
#endif
            // 檢查帳號是否鎖定
            int failLoginTime = 15;
            int failLoginCount = 5;
            if (loginLogService.HasExceededFailedLoginAttempts(model.Name, failLoginTime, failLoginCount))
            {
                ModelState.AddModelError(nameof(model.ErrorMessage), $"登入已超過{failLoginCount}次上限，請{failLoginTime}分鐘後再嘗試");
                _ = this.accountService.LockUserByTime(model.Name, DateTime.Now.AddMinutes(failLoginTime));
            }

            // 檢查帳號密碼
            model.Password = Crypto.Aes.Encrypt(model.Password);
            if (!accountService.IsLogin(mapper.Map<UserModel>(model)))
            {
                ModelState.AddModelError(nameof(model.ErrorMessage), "登入失敗，請檢查您的帳號和密碼。");

                loginLogModel.IS_SUCCESSFUL = false;
                this.loginLogService.AddLoginLog(loginLogModel);
            }

            // 檢查通過
            if (ModelState.IsValid)
            {
                List<Claim> claims = new() { new Claim(ClaimTypes.Name, model.Name) };

                ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                loginLogModel.IS_SUCCESSFUL = true;
                this.loginLogService.AddLoginLog(loginLogModel);

                if (!string.IsNullOrEmpty(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        private RecaptchaResultModel CheckRecaptcha(string response)
        {
            HttpRequestMessage request = new()
            {
                RequestUri = new Uri(recaptchaModel.GetUrl(response))
            };
            HttpClient httpClient = new();

            HttpResponseMessage httpResponseMessage = httpClient.Send(request);
            string result = httpResponseMessage.Content.ReadAsStringAsync().Result ?? string.Empty;

            RecaptchaResultModel model = JsonConvert.DeserializeObject<RecaptchaResultModel>(result) ?? new RecaptchaResultModel();

            return model;
        }

        #endregion

        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        #region 修改密碼

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public ActionResult ChangePassword()
        {
            if (User.Identity is null || string.IsNullOrEmpty(User.Identity.Name))
                return View("Login");

            return View(new ChangePasswordViewModel()
            {
                Name = User.Identity.Name
            });
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            ViewBag.Recaptcha = recaptchaModel;

            ValidationResult validator = passwordValidator.Validate(model);
            if (!validator.IsValid)
                validator.Errors.ForEach(error => ModelState.AddModelError(nameof(model.ErrorMessages), error.ErrorMessage));

            if (ModelState.ErrorCount > 0)
                return View(model);

            model.Password = Crypto.Aes.Encrypt(model.Password);
            model.NewPassword = Crypto.Aes.Encrypt(model.NewPassword);

            if (await accountService.UpdatePassword(mapper.Map<UserModel>(model)))
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return View("Login");
            }
            else
            {
                ModelState.AddModelError(nameof(model.ErrorMessages), "修改密碼失敗");
                return View(model);
            }

        }

        #endregion

    }
}
