/* MIT License
 * Copyright (c) 2016 Sun Bo
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
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
        public static IDbConnection KeepConnection(IDbConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

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
