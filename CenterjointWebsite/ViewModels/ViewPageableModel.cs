namespace YCInterview.Web.ViewModels
{
    using Domain.Models;

    public class ViewPageableModel : PageableModel
    {
        public Dictionary<string, string>? GroupedData { get; set; } = new Dictionary<string, string>();
    }

    public class SuccessCasePageable : ViewPageableModel
    {
        public SuccessCasePageable()
        {
            this.PageNumber = 1;
            this.PageSize = 6;
        }

        public SuccessCasePageable(int page)
        {
            this.PageNumber = page;
            this.PageSize = 6;
        }

        public SuccessCasePageable(int pageSize, int page, int? typeId)
        {
            this.PageSize = pageSize;
            this.PageNumber = page;
            GroupedData =
                new Dictionary<string, string>
                {
                    {"typeId",typeId?.ToString()??"" }
                };
        }
    }

}
