namespace Domain.Implements
{
    using Domain.Dacs;
    using Domain.Interfaces;
    using Domain.Models;
    using Infrastructure;
    using System;
    using System.Collections.Generic;

    public class LoginLogDac : BaseDac, ILoginLogDac
    {
        public LoginLogDac(IApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public Task<int> Insert(LoginLogModel model)
        {
            string sql = @"  INSERT INTO USER_LOGIN_LOG
                                    (NAME,  IS_SUCCESSFUL)
                                VALUES
                                    (@NAME, @IS_SUCCESSFUL)";

            return base.ExcuteAsync(sql, model);
        }

        public Task<IList<LoginLogModel>> QueryLogs(
            string userName,
            bool isSuccess,
            DateTime? startLogTime = null,
            DateTime? endLogTime = null)
        {
            string sql = @$"  SELECT 
	                                NAME
	                                ,IS_SUCCESSFUL
                                FROM USER_LOGIN_LOG
                                WHERE NAME = @{nameof(userName)}
                                    AND IS_SUCCESSFUL = @{nameof(isSuccess)}";

            if (startLogTime is not null)
                sql += $" AND LOG_TIME >= @{nameof(startLogTime)}";

            if (endLogTime is not null)
                sql += $" AND LOG_TIME <= @{nameof(endLogTime)}";


            return base.ExcuteQueryAsync<LoginLogModel>(sql, new
            {
                userName,
                isSuccess = isSuccess ? 1 : 0,
                startLogTime = startLogTime.HasValue ? startLogTime.Value.ToUtcTimeFormat() : null,
                endLogTime = endLogTime.HasValue ? endLogTime.Value.ToUtcTimeFormat() : null
            });
        }
    }
}
