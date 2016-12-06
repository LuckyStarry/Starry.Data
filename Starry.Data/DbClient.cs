using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
#if NET20 || NET30
using Starry.Data.Extension;
#endif

namespace Starry.Data
{
    public class DbClient : IDbClient
    {
        public DbClient(string dbName)
        {
            this.DBName = dbName;
        }

        public string DBName { private set; get; }

        public virtual IDbConnection CreateDbConnection()
        {
            return DbHelper.CreateDbConnection(this.DBName);
        }

        public static T Execute<T>(IDbConnection connection, Func<IDbConnection, T> func)
        {
            using (connection)
            {
                return func(connection);
            }
        }

        public T Execute<T>(Func<IDbConnection, T> func)
        {
            return DbClient.Execute<T>(this.CreateDbConnection(), func);
        }

        public int ExecuteNonQuery(string sqlText, object param = null)
        {
            return this.Execute(connection => connection.ExecuteNonQuery(sqlText, param));
        }

        public T ExecuteScalar<T>(string sqlText, object param = null)
        {
            return this.Execute(connection => connection.ExecuteScalar<T>(sqlText, param));
        }

        public IEnumerable<T> Query<T>(string sqlText, object param = null)
        {
            return this.Execute(connection => connection.Query<T>(sqlText, param));
        }
    }
}
