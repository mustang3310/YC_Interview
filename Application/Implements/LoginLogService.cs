namespace Application.Implements
{
    using Application.Interfaces;
    using Domain.Interfaces;
    using Domain.Models;

    public class LoginLogService : BaseService, ILoginLogService
    {
        private readonly ILoginLogDac loginDac;

        public LoginLogService(ILoginLogDac dac)
            => loginDac = dac;

        public void AddLoginLog(LoginLogModel model)
            => _ = this.loginDac.Insert(model);

        public bool HasExceededFailedLoginAttempts(string userName, int timeSpanMinutes, int maxFailedAttempts)
        {
            DateTime startTime = DateTime.Now.AddMinutes(-timeSpanMinutes);
            DateTime endTime = DateTime.Now;
            bool isOverCount = this.loginDac.QueryLogs(userName, false, startTime, endTime).Result.Count >= maxFailedAttempts;

            return isOverCount;
        }
    }
}
