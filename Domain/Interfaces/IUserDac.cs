namespace Domain.Interfaces
{
    using Domain.Models;
    using System.Threading.Tasks;

    public interface IUserDac
    {
        /// <summary>
        /// 確認帳號與密碼是否相符
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<bool> CheckNameMatchPassword(UserModel model);

        /// <summary>
        ///  以 Name 跟 舊密碼 來更新新密碼
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<bool> UpdatePasswordByNameAndOldPassword(UserModel model);

        /// <summary>
        /// 確認使用者是否被鎖定
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsUserLocked(string userName);

        /// <summary>
        /// 鎖定使用者
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public bool LockUser(string userName, DateTime expireTime);
    }
}
