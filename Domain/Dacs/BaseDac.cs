namespace Domain.Dacs
{
    using Dapper;
    using Infrastructure;

    public class BaseDac
    {
        private readonly IApplicationDbContext context;

        public BaseDac(IApplicationDbContext applicationDbContext)
        {
            this.context = applicationDbContext;
        }

        protected int Excute(string sql, object objParameter)
        {
            context.GetDbConnection().Open();

            if (context.IsTran())
            {
                return context.GetDbConnection().Execute(sql, objParameter, context.GetTransaction());
            }
            else
            {
                return context.GetDbConnection().Execute(sql, objParameter);
            }
        }

        public bool Excute(string sql)
        {
            if (context.IsTran())
                return context.GetDbConnection().Execute(sql, null, context.GetTransaction()) > 0;
            else
                return context.GetDbConnection().Execute(sql) > 0;
        }

        public async Task<int> ExcuteAsync(string sql, object? parameter = null)
        {
            if (context.IsTran())
            {
                return await context.GetDbConnection().ExecuteAsync(sql, parameter, context.GetTransaction());
            }
            else
            {
                return await context.GetDbConnection().ExecuteAsync(sql, parameter);
            }
        }

        protected IList<T> ExcuteQuery<T>(string sql, object? objParameter = null)
        {
            context.GetDbConnection().Open();

            if (context.IsTran())
            {
                return context.GetDbConnection().Query<T>(sql, objParameter, context.GetTransaction()).ToList();
            }
            else
            {
                return context.GetDbConnection().Query<T>(sql, objParameter).ToList();
            }
        }

        protected async Task<IList<T>> ExcuteQueryAsync<T>(string sql, object? parameter = null)
        {
            context.GetDbConnection().Open();

            IEnumerable<T> result;

            if (context.IsTran())
            {
                result = await context.GetDbConnection().QueryAsync<T>(sql, parameter, context.GetTransaction());
            }
            else
            {
                result = await context.GetDbConnection().QueryAsync<T>(sql, parameter);
            }

            return result.ToList();
        }
    }
}