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
    /// The DbClient is able to execute sql commands and manage connections and have some advanced features.
    /// </summary>
    public abstract class DbClientAdvanced : IDbClient, IDbClientQueryPageable
    {
        /// <summary>
        /// Create a DbClient with a normal dbClient.
        /// </summary>
        /// <param name="dbClient">a normal dbClient.</param>
        public DbClientAdvanced(IDbClient dbClient)
        {
            this.dbClient = dbClient;
        }

        private IDbClient dbClient;
        /// <summary>
        /// Gets the database's name.
        /// </summary>
        public string DBName
        {
            get { return this.dbClient.DBName; }
        }
        /// <summary>
        /// Create a new connection from the database.
        /// </summary>
        /// <returns>A new connection from the database.</returns>
        public IDbConnection CreateDbConnection()
        {
            return this.dbClient.CreateDbConnection();
        }
        /// <summary>
        /// Execute a function with a new connection
        /// </summary>
        /// <typeparam name="T">The type of result</typeparam>
        /// <param name="func">a function executed with a connection</param>
        /// <returns>Execute result</returns>
        public T Execute<T>(Func<IDbConnection, T> func)
        {
            return this.Execute<T>(func);
        }
        /// <summary>
        /// Executes an SQL statement against the Connection object of a .NET Framework data provider, and returns the number of rows affected.
        /// </summary>
        /// <param name="sqlText">The text command to execute.</param>
        /// <param name="param">The parameters of the SQL statement or stored procedure.</param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteNonQuery(string sqlText, object param = null)
        {
            return this.ExecuteNonQuery(sqlText, param);
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
            return this.ExecuteScalar<T>(sqlText, param);
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
            return this.Query<T>(sqlText, param);
        }
        /// <summary>
        /// Execute a sql command then get the recordset with paging.
        /// </summary>
        /// <typeparam name="T">The type of elements</typeparam>
        /// <param name="sqlText">The text command to execute.</param>
        /// <param name="total">The number of row's count before paging.</param>
        /// <param name="pageIndex">Page's index based 1.</param>
        /// <param name="pageSize">The number of row's count with one page.</param>
        /// <param name="param">The parameters of the SQL statement or stored procedure.</param>
        /// <returns>Execute result</returns>
        public IEnumerable<T> Query<T>(string sqlText, out int total, int pageIndex = 1, int pageSize = 20, object param = null)
        {
            return this.Query<T>(sqlText, param, out total, pageIndex, pageSize);
        }
        /// <summary>
        /// Execute a sql command then get the recordset with paging.
        /// </summary>
        /// <typeparam name="T">The type of elements</typeparam>
        /// <param name="sqlText">The text command to execute.</param>
        /// <param name="param">The parameters of the SQL statement or stored procedure.</param>
        /// <param name="total">The number of row's count before paging.</param>
        /// <param name="pageIndex">Page's index based 1.</param>
        /// <param name="pageSize">The number of row's count with one page.</param>
        /// <returns>Execute result</returns>
        public abstract IEnumerable<T> Query<T>(string sqlText, object param, out int total, int pageIndex = 1, int pageSize = 20);
    }
}
