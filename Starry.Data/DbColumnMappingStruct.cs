using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Starry.Data
{
    class DbColumnMappingStruct<T> : DbColumnMapping<T> where T : struct
    {
        public override T GetValue(System.Data.IDataReader dataReader)
        {
            var entity = Activator.CreateInstance<T>();
            var value = dataReader.GetValue(0);
            if (value == null || value is DBNull)
            {
                entity = default(T);
            }
            else
            {
                var underlyingType = Nullable.GetUnderlyingType(typeof(T));
                entity = (T)Convert.ChangeType(value, underlyingType ?? typeof(T));
            }
            return entity;
        }
    }
}
