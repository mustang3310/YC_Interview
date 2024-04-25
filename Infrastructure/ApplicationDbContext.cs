namespace Infrastructure
{
    using Microsoft.Data.Sqlite;
    using Microsoft.Extensions.Configuration;
    using System.Data;
    using System.Data.Common;

    public class ApplicationDbContext : IApplicationDbContext, IDisposable
    {
        private bool disposedValue;
        private readonly DbConnection _connection;

        //Transaction參數
        protected bool _useTranscation { get; set; } = false;
        //DbTransaction
        protected DbTransaction? _tran { get; set; }

        public ApplicationDbContext(IConfiguration configuration)
        {
            IConfiguration _configuration = configuration;
            String connectionString = _configuration?.GetConnectionString("Default") ?? throw new ArgumentNullException(nameof(configuration));
            _connection = new SqliteConnection(connectionString);
        }

        public DbConnection GetDbConnection() => _connection;

        protected virtual void Dispose(Boolean disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //  處置受控狀態 (受控物件)
                }

                //  釋出非受控資源 (非受控物件) 並覆寫完成項
                //  將大型欄位設為 Null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // 請勿變更此程式碼。請將清除程式碼放入 'Dispose(bool disposing)' 方法
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 是否開啟交易
        /// </summary>
        public bool IsTran() => _useTranscation;

        /// <summary>
        /// 取得DbTransaction
        /// </summary>
        /// <returns></returns>
        public DbTransaction GetTransaction()
        {
            //若不使用Transcation機制則回傳null
            if (_useTranscation)
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                _tran ??= _connection.BeginTransaction();
            }
            return _tran;
        }

        /// <summary>
        /// 使用交易機制
        /// </summary>
        public void BeginTransaction()
        {
            _useTranscation = true;
        }

        /// <summary>
        /// Transaction Commit
        /// </summary>
        public virtual void Commit()
        {
            _useTranscation = false;
            GetTransaction()?.Commit();
            GetTransaction()?.Dispose();
            GetDbConnection()?.Close();
            _tran = null;
        }

        /// <summary>
        /// Transaction Rollback
        /// </summary>
        public virtual void Rollback()
        {
            _useTranscation = false;
            GetTransaction()?.Rollback();
            GetTransaction()?.Dispose();
            GetDbConnection()?.Close();
            _tran = null;
        }
    }
}
