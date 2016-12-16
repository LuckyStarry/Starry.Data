using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Starry.Data
{
    /// <summary>
    /// Represents a object used to generate <see cref="Starry.Data.IDbColumnMapping"/>
    /// </summary>
    public interface IDbColumnMappingFactory
    {
        /// <summary>
        /// Generate <see cref="Starry.Data.IDbColumnMapping"/> by the type of Data and <see cref="System.Data.IDataReader"/>
        /// </summary>
        /// <param name="type">Type of Data</param>
        /// <param name="reader"><see cref="System.Data.IDataReader"/></param>
        /// <returns><see cref="Starry.Data.IDbColumnMapping"/></returns>
        IDbColumnMapping GetDbColumnMapping(Type type, IDataReader reader);
    }
}
