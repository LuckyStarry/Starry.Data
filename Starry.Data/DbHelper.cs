using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Starry.Data
{
    abstract class DbHelper
    {
        private DbHelper() { }

        public static IDbConnection CreateDbConnection(string dbName)
        {
            var config = ConfigurationManager.ConnectionStrings[dbName];
            if (config == null)
            {
                throw new Exception(string.Format("Database {0}'s connection string is not exist.", dbName));
            }
            var provider = DbProviderFactories.GetFactory(config.ProviderName);
            var connection = provider.CreateConnection();
            connection.ConnectionString = config.ConnectionString;
            return connection;
        }

        public static IDbDataParameter CreateParameter(IDbCommand command, string name, object value)
        {
            var dbParam = command.CreateParameter();
            dbParam.ParameterName = name;
            dbParam.Value = value;
            return dbParam;
        }

        public static IEnumerable<IDbDataParameter> CreateParameters(IDbCommand command, object param)
        {
            if (param == null)
            {
                return null;
            }
            if (param is IDbDataParameter)
            {
                return new IDbDataParameter[] { param as IDbDataParameter };
            }
            if (param is IEnumerable<IDbDataParameter>)
            {
                return param as IEnumerable<IDbDataParameter>;
            }
            return DbHelper.EntityToParameters(command, param);
        }

        private static IEnumerable<IDbDataParameter> EntityToParameters(IDbCommand command, object param)
        {
            foreach (var property in param.GetType().GetProperties())
            {
                if (property.CanRead)
                {
                    var value = property.GetValue(param, null);
                    yield return DbHelper.CreateParameter(command, "@" + property.Name, value);
                }
            }
        }

        public static IDbCommand CreateCommand(IDbConnection connection, string sqlText, object param = null)
        {
            var command = connection.CreateCommand();
            command.CommandText = sqlText;
            if (param != null)
            {
                var dbParams = DbHelper.CreateParameters(command, param);
                if (dbParams != null)
                {
                    foreach (var dbParam in dbParams)
                    {
                        command.Parameters.Add(dbParam);
                    }
                }
            }
            return command;
        }
    }
}
