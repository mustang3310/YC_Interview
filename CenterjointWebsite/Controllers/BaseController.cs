namespace YCInterview.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// 所有的controller都應繼承此類別，或是繼承其子類別
    /// </summary>
    public class BaseController : Controller
    {
        protected readonly IMapper mapper;

        public BaseController() { }

        public BaseController(IMapper mapper) => this.mapper = mapper;

        public override async void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
