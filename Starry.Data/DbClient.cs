using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
#if NET20 || NET30
using Starry.Data.Extension;
#endif

namespace Starry.Data
{
    /// <summary>
    /// The DbClient is able to execute sql commands and manage connections.
    /// </summary>
    public class DbClient : IDbClient
    {
        /// <summary>
        /// Create a DbClient with database's name.
        /// </summary>
        /// <param name="dbName"></param>
        public DbClient(string dbName)
        {
            this.DBName = dbName;
        }
        /// <summary>
        /// Gets the database's name.
        /// </summary>
        public string DBName { private set; get; }
        /// <summary>
        /// Create a new connection from the database.
        /// </summary>
        /// <returns>A new connection from the database.</returns>
        public virtual IDbConnection CreateDbConnection()
        {
            return DbHelper.CreateDbConnection(this.DBName);
        }
        /// <summary>
        /// Execute a function with connection, then the connection will be close.
        /// </summary>
        /// <typeparam name="T">The type of result</typeparam>
        /// <param name="connection">A connection use for execute a function</param>
        /// <param name="func">a function executed with a connection</param>
        /// <returns>Execute result</returns>
        public static T Execute<T>(IDbConnection connection, Func<IDbConnection, T> func)
        {
            using (connection)
            {
                return func(connection);
            }
        }
        /// <summary>
        /// Execute a function with a new connection
        /// </summary>
        /// <typeparam name="T">The type of result</typeparam>
        /// <param name="func">a function executed with a connection</param>
        /// <returns>Execute result</returns>
        public T Execute<T>(Func<IDbConnection, T> func)
        {
            return DbClient.Execute<T>(this.CreateDbConnection(), func);
        }
        /// <summary>
        /// Executes an SQL statement against the Connection object of a .NET Framework data provider, and returns the number of rows affected.
        /// </summary>
        /// <param name="sqlText">The text command to execute.</param>
        /// <param name="param">The parameters of the SQL statement or stored procedure.</param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteNonQuery(string sqlText, object param = null)
        {
            return this.Execute(connection => connection.ExecuteNonQuery(sqlText, param));
        }
        /// <summary>
        /// Executes the query, and returns the first column of the first row in the resultset returned by the query. Extra columns or rows are ignored.
        /// </summary>
        /// <typeparam name="T">The type of result</typeparam>
        /// <param name="sqlText">The text command to execute.</param>
        /// <param name="param">The parameters of the SQL statement or stored procedure.</param>
        /// <returns>The first column of the first row in the resultset.</returns>
        public T ExecuteScalar<T>(string sqlText, object param = null)
        {
            return this.Execute(connection => connection.ExecuteScalar<T>(sqlText, param));
        }
        /// <summary>
        /// Execute a sql command then get the recordset.
        /// </summary>
        /// <typeparam name="T">The type of elements</typeparam>
        /// <param name="sqlText">The text command to execute.</param>
        /// <param name="param">The parameters of the SQL statement or stored procedure.</param>
        /// <returns>Execute result</returns>
        public IEnumerable<T> Query<T>(string sqlText, object param = null)
        {
            return this.Execute(connection => connection.Query<T>(sqlText, param));
        }
    }
}
