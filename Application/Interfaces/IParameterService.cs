namespace Application.Interfaces
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IParameterService
    {
        /// <summary>
        /// 依照 type Name 取得 參數資料 並轉成 SelectListItem
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> QueryParametersByType(string typeName);

    }
}
