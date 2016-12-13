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
        public static IDbConnection KeepConnection(this IDbConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }
    }
}
