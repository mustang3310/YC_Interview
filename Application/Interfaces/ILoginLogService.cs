namespace Application.Interfaces
{
    using Domain.Models;

    /// <summary>
    /// 帳號登入紀錄
    /// </summary>
    public interface ILoginLogService
    {
        /// <summary>
        /// 新增帳號登入 Log
        /// </summary>
        /// <param name="model"></param>
        public void AddLoginLog(LoginLogModel model);

        /// <summary>
        /// 是否超過帳號登入錯誤次數
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="timeSpanMinutes">檢查的時間範圍（例如，最近的 15 分鐘）</param>
        /// <param name="maxFailedAttempts">最大允許的失敗登入嘗試次數</param>
        /// <returns></returns>
        public bool HasExceededFailedLoginAttempts(string userName, int timeSpanMinutes, int maxFailedAttempts);
    }
}
