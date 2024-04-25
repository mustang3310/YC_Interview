namespace YCInterview.Web.Widget
{
    using Domain.Models;

    public class Pager : PageableModel
    {
        private const int pageSize = 9;
        private const int pageNumber = 1;

        public Pager(PageableModel pageable)
        {
            this.TotalCount = pageable.TotalCount;
            this.PageNumber = pageable.PageNumber == 0 ? pageNumber : pageable.PageNumber;
            this.PageSize = pageable.PageSize == 0 ? pageSize : pageable.PageSize;
        }

        public Pager()
        {
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
        }

        /// <summary>
        /// 總頁數
        /// </summary>
        public int PageCount => this.TotalCount / this.PageSize + (this.TotalCount % this.PageSize > 0 ? 1 : 0);

    }
}
