namespace Domain.Models
{

    public class PageableModel
    {
        /// <summary>
        /// 總筆數
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 目前頁數
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 每頁筆數
        /// </summary>
        public int PageSize { get; set; }

    }

}
