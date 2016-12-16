using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Starry.Data
{
    public interface IDbColumnMappingFactory
    {
        IDbColumnMapping GetDbColumnMapping(Type type, IDataReader reader);
    }
}
