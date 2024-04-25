namespace Application.Implements
{
    using Application.Interfaces;
    using Domain.Interfaces;
    using Domain.Models;

    public class AccountService : BaseService, IAccountService
    {
        private readonly IUserDac userDac;

        public AccountService(IUserDac dac)
        {
            userDac = dac;
        }

        public bool IsLogin(UserModel model)
        {
            if (userDac.CheckNameMatchPassword(model).Result
                && !userDac.IsUserLocked(model.NAME)
                )
            {
                return true;
            }

            return false;
        }

        public Task<bool> UpdatePassword(UserModel model)
            => userDac.UpdatePasswordByNameAndOldPassword(model);

        public bool LockUserByTime(string userName, DateTime expireTime)
            => userDac.LockUser(userName, expireTime);
    }
}
