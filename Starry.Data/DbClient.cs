using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
#if NET20 || NET30
using Starry.Data.Extension;
#endif
using System.Reflection;

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
        /// Execute a function with a new connection
        /// </summary>
        /// <typeparam name="T">The type of result</typeparam>
        /// <param name="func">a function executed with a connection</param>
        /// <returns>Execute result</returns>
        public T Execute<T>(Func<IDbConnection, T> func)
        {
            using (var connection = this.CreateDbConnection())
            {
                return func(connection);
            }
        }
        /// <summary>
        /// Executes an SQL statement against the Connection object of a .NET Framework data provider, and returns the number of rows affected.
        /// </summary>
        /// <param name="sqlText">The text command to execute.</param>
        /// <param name="param">The parameters of the SQL statement or stored procedure.</param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteNonQuery(string sqlText, object param = null)
        {
            return this.Execute(connection =>
            {
                DbHelper.KeepConnection(connection);
                var command = DbHelper.CreateCommand(connection, sqlText, param);
                return command.ExecuteNonQuery();
            });
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
            return this.Execute(connection =>
            {
                DbHelper.KeepConnection(connection);
                var command = DbHelper.CreateCommand(connection, sqlText, param);
                var value = command.ExecuteScalar();
                if (value == null)
                {
                    return default(T);
                }
                return (T)Convert.ChangeType(value, typeof(T));
            });
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
            using (var connection = this.CreateDbConnection())
            {
                DbHelper.KeepConnection(connection);
                var command = DbHelper.CreateCommand(connection, sqlText, param);
                using (var dataReader = command.ExecuteReader())
                {
                    var mappings = new Dictionary<string, PropertyInfo>();
                    for (var i = 0; i < dataReader.FieldCount; i++)
                    {
                        var colName = dataReader.GetName(i);
                        var property = typeof(T).GetProperty(colName);
                        if (property != null && property.CanWrite)
                        {
                            mappings[colName] = property;
                        }
                    }
                    while (dataReader.Read())
                    {
                        var entity = Activator.CreateInstance<T>();
                        foreach (var mapping in mappings)
                        {
                            var value = dataReader[mapping.Key];
                            if (value == null || value is DBNull)
                            {
                                mapping.Value.SetValue(entity, null, null);
                            }
                            else
                            {
                                var underlyingType = Nullable.GetUnderlyingType(mapping.Value.PropertyType);
                                mapping.Value.SetValue(entity, Convert.ChangeType(value, underlyingType ?? mapping.Value.PropertyType), null);
                            }
                        }
                        yield return entity;
                    }
                }
            }
        }
    }
}
