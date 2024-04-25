namespace Domain.Interfaces
{
    using Domain.Models;

    public interface ILoginLogDac
    {
        public Task<int> Insert(LoginLogModel model);

        public Task<IList<LoginLogModel>> QueryLogs(
            string userName,
            bool isSuccess,
            DateTime? startLogTime = null,
            DateTime? endLogTime = null);
    }
}
