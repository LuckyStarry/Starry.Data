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
    static partial class IDbConnectionExtension
    {
        static IDbConnection KeepConnection(this IDbConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

        public static int ExecuteNonQuery(this IDbConnection connection, string sqlText, object param = null)
        {
            var command = DbHelper.CreateCommand(connection.KeepConnection(), sqlText, param);
            return command.ExecuteNonQuery();
        }

        public static T ExecuteScalar<T>(this IDbConnection connection, string sqlText, object param = null)
        {
            var command = DbHelper.CreateCommand(connection.KeepConnection(), sqlText, param);
            var value = command.ExecuteScalar();
            if (value == null)
            {
                return default(T);
            }
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static IEnumerable<T> Query<T>(this IDbConnection connection, string sqlText, object param = null)
        {
            var command = DbHelper.CreateCommand(connection.KeepConnection(), sqlText, param);
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
                        if (value == null)
                        {
                            mapping.Value.SetValue(entity, value, null);
                        }
                        else
                        {
                            mapping.Value.SetValue(entity, Convert.ChangeType(value, mapping.Value.PropertyType), null);
                        }
                    }
                    yield return entity;
                }
            }
        }
    }
}
