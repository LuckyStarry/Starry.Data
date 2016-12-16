using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Starry.Data
{
    class DbColumnMappingFactory : IDbColumnMappingFactory
    {
        public virtual IDbColumnMapping GetDbColumnMapping(Type type, IDataReader reader)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(typeof(DbColumnMappingStruct<>).MakeGenericType(type)) as IDbColumnMapping;
            }
            return Activator.CreateInstance(typeof(DbColumnMappingReference<>).MakeGenericType(type), reader.GetSchemaTable()) as IDbColumnMapping;
        }
    }
}
