using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Starry.Data
{
    abstract class DbColumnMapping<T> : IDbColumnMapping, IDbColumnMapping<T>
    {
        public abstract T GetValue(IDataReader dataReader);

        object IDbColumnMapping.GetValue(IDataReader dataReader)
        {
            return this.GetValue(dataReader);
        }
    }
}
