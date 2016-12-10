using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Starry.Data
{
    /// <summary>
    /// Represents a DbClient is able to execute a sql command then get the recordset with paging.
    /// </summary>
    public interface IDbClientQueryPageable : IDbClient
    {
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
        IEnumerable<T> Query<T>(string sqlText, out int total, int pageIndex = 1, int pageSize = 20, object param = null);
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
        IEnumerable<T> Query<T>(string sqlText, object param, out int total, int pageIndex = 1, int pageSize = 20);
    }
}
