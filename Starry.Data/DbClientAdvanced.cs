using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
#if NET20 || NET30
using Starry.Data.Extension;
#endif

namespace Starry.Data
{
    public abstract class DbClientAdvanced : IDbClient, IDbClientQueryPageable
    {
        public DbClientAdvanced(IDbClient dbClient)
        {
            this.dbClient = dbClient;
        }

        private IDbClient dbClient;

        public string DBName
        {
            get { return this.dbClient.DBName; }
        }

        public IDbConnection CreateDbConnection()
        {
            return this.dbClient.CreateDbConnection();
        }

        public T Execute<T>(Func<IDbConnection, T> func)
        {
            return this.Execute<T>(func);
        }

        public int ExecuteNonQuery(string sqlText, object param = null)
        {
            return this.ExecuteNonQuery(sqlText, param);
        }

        public T ExecuteScalar<T>(string sqlText, object param = null)
        {
            return this.ExecuteScalar<T>(sqlText, param);
        }

        public IEnumerable<T> Query<T>(string sqlText, object param = null)
        {
            return this.Query<T>(sqlText, param);
        }

        public T QueryFirst<T>(string sqlText, object param = null)
        {
            return this.QueryFirst<T>(sqlText, param);
        }

        public T QueryFirstOrDefault<T>(string sqlText, object param = null)
        {
            return this.QueryFirstOrDefault<T>(sqlText, param);
        }

        public IEnumerable<T> Query<T>(string sqlText, out int total, int pageIndex = 1, int pageSize = 20, object param = null)
        {
            return this.Query<T>(sqlText, param, out total, pageIndex, pageSize);
        }

        public abstract IEnumerable<T> Query<T>(string sqlText, object param, out int total, int pageIndex = 1, int pageSize = 20);
    }
}
