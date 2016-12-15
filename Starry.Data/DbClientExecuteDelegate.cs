using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Starry.Data
{
    /// <summary>
    /// The delegate for SQL command(s) executing.
    /// </summary>
    /// <typeparam name="T">The type of result.</typeparam>
    /// <param name="connection">The database connection used for execute SQL command(s).</param>
    /// <returns>The result of executing.</returns>
    public delegate T DbClientExecuteDelegate<T>(IDbConnection connection);
}
