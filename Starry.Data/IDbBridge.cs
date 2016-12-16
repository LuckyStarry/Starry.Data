using System;
using System.Collections.Generic;
using System.Text;

namespace Starry.Data
{
    /// <summary>
    /// Represents a object which used to bridge Starry.Data's features.
    /// </summary>
    public interface IDbBridge
    {
        /// <summary>
        /// Gets and sets the factory used to generate <see cref="Starry.Data.IDbColumnMapping"/>
        /// </summary>
        IDbColumnMappingFactory MappingFactory { set; get; }
    }
}
