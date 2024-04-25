namespace Domain.Interfaces
{
    using Domain.Models;

    public interface IPageDac
    {
        public IList<PageModel> QueryPageModelsByGroupId(int groupId);

        public PageModel GetPageModel(int id);

        public PageModel? GetPageModelByUrl(string url);

        public int CountPageModelByUrl(string url);

        public int Insert(PageModel pageModel);

        public int UpdateOrdinalNumberAndVisible(IList<PageModel> pageModels);

        public int UpdatePageInfo(PageModel pageModels);

        public int Delete(int id);

        public IList<PageModel> GetPageModelVisibleAndNotDeleteByGroupId(int groupId);

        public Task<IList<int>> QueryInsertId();

    }
}
