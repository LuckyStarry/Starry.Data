using System;
using System.Collections.Generic;
using System.Text;

namespace Starry.Data
{
    /// <summary>
    /// Represents a DbBridge is able to provide some features for database operation.
    /// </summary>
    public interface IDbBridge
    {
        IDbColumnMappingFactory MappingFactory { set; get; }
    }
}
