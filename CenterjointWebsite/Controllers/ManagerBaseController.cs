namespace YCInterview.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using OfficialWebsite.Core.Helper;
    using OfficialWebsite.Core.Services;

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ManagerBaseController : BaseController
    {
        protected ConfigurationService configurationService;
        protected IFileHelper fileService;
        public ManagerBaseController(
            IMapper mapper,
            ConfigurationService configurationService,
            IFileHelper fileHelper) : base(mapper)
        {
            this.configurationService = configurationService;
            this.fileService = fileHelper;
        }
    }
}
