namespace Infrastructure
{
    using System.Data.Common;

    public interface IApplicationDbContext
    {
        public DbConnection GetDbConnection();

        public bool IsTran();

        public DbTransaction GetTransaction();

        public void BeginTransaction();

        public void Commit();

        public void Rollback();
    }
}
