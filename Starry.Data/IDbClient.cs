using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
#if NET20 || NET30
using Starry.Data.Extension;
#endif

namespace Starry.Data
{
    public interface IDbClient
    {
        string DBName { get; }
        IDbConnection CreateDbConnection();
        T Execute<T>(Func<IDbConnection, T> func);
        int ExecuteNonQuery(string sqlText, object param = null);
        T ExecuteScalar<T>(string sqlText, object param = null);
        IEnumerable<T> Query<T>(string sqlText, object param = null);
    }
}
