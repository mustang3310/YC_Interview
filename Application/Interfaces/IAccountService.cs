namespace Application.Interfaces
{
    using Domain.Models;

    /// <summary>
    /// 帳號相關服務
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 確認帳號是否可以登入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool IsLogin(UserModel model);

        /// <summary>
        /// 確認 Name 跟 舊密碼後 來更新密碼
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<bool> UpdatePassword(UserModel model);

        /// <summary>
        /// 將 帳號 鎖定至設定時間
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public bool LockUserByTime(string userName, DateTime expireTime);
    }
}
