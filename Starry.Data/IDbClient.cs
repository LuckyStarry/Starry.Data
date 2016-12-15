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
    /// Represents a DbClient is able to execute sql commands and manage connections.
    /// </summary>
    public interface IDbClient
    {
        /// <summary>
        /// Gets the database's name.
        /// </summary>
        string DBName { get; }
        /// <summary>
        /// Create a new connection from the database.
        /// </summary>
        /// <returns>A new connection from the database.</returns>
        IDbConnection CreateDbConnection();
        /// <summary>
        /// Execute a function with a new connection
        /// </summary>
        /// <typeparam name="T">The type of result</typeparam>
        /// <param name="func">a function executed with a connection</param>
        /// <returns>Execute result</returns>
        T Execute<T>(DbClientExecuteDelegate<T> func);
        /// <summary>
        /// Executes an SQL statement against the Connection object of a .NET Framework data provider, and returns the number of rows affected.
        /// </summary>
        /// <param name="sqlText">The text command to execute.</param>
        /// <param name="param">The parameters of the SQL statement or stored procedure.</param>
        /// <returns>The number of rows affected.</returns>
        int ExecuteNonQuery(string sqlText, object param = null);
        /// <summary>
        /// Executes the query, and returns the first column of the first row in the resultset returned by the query. Extra columns or rows are ignored.
        /// </summary>
        /// <typeparam name="T">The type of result</typeparam>
        /// <param name="sqlText">The text command to execute.</param>
        /// <param name="param">The parameters of the SQL statement or stored procedure.</param>
        /// <returns>The first column of the first row in the resultset.</returns>
        T ExecuteScalar<T>(string sqlText, object param = null);
        /// <summary>
        /// Execute a sql command then get the recordset.
        /// </summary>
        /// <typeparam name="T">The type of elements</typeparam>
        /// <param name="sqlText">The text command to execute.</param>
        /// <param name="param">The parameters of the SQL statement or stored procedure.</param>
        /// <returns>Execute result</returns>
        IEnumerable<T> Query<T>(string sqlText, object param = null);
    }
}
