using Domain.Dacs;

namespace Domain.Implements
{
    using Domain.Interfaces;
    using Domain.Models;
    using Infrastructure;

    public class UserDac : BaseDac, IUserDac
    {
        public UserDac(IApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public async Task<bool> CheckNameMatchPassword(UserModel model)
        {
            string sql = @"SELECT 
                                CASE WHEN COUNT(*) = 1 
                                THEN 1 ELSE 0 END 
                                AS [NAME]
                            FROM USER 
                            WHERE 
                                NAME = @NAME 
                                AND PASSWORD = @PASSWORD";

            return (await ExcuteQueryAsync<bool>(sql, model)).FirstOrDefault();
        }

        public async Task<bool> UpdatePasswordByNameAndOldPassword(UserModel model)
        {
            string sql = @"UPDATE USER 
                                SET PASSWORD = @NEWPASSWORD 
                            WHERE 
                                NAME = @NAME 
                                AND PASSWORD = @PASSWORD";

            return await ExcuteAsync(sql, model) == 1;
        }

        public bool IsUserLocked(string userName)
        {
            string sql = @$"SELECT 
                                CASE WHEN 
                                    LOCK_TIME >= DATETIME() THEN 1 
                                    ELSE 0 
                                END
                            FROM USER
                            WHERE NAME = @{nameof(userName)}";
            bool isLocked = base.ExcuteQueryAsync<bool>(sql, new { userName }).Result.Single();
            return isLocked;
        }

        public bool LockUser(string userName, DateTime expireTime)
        {
            string sql = $@"UPDATE USER 
                            SET LOCK_TIME = @{nameof(expireTime)} 
                            WHERE NAME = @{nameof(userName)}";

            int result =
                this.ExcuteAsync(sql, new
                {
                    userName,
                    expireTime = expireTime.ToUtcTimeFormat()
                }).Result;

            return result == 1;
        }
    }
}
