using System;
namespace Starry.Data
{
    /// <summary>
    /// Represents a object used to get the value from <see cref="System.Data.IDataReader"/>.
    /// </summary>
    public interface IDbColumnMapping
    {
        /// <summary>
        /// Gets the value from <see cref="System.Data.IDataReader"/>.
        /// </summary>
        /// <param name="dataReader"><see cref="System.Data.IDataReader"/></param>
        /// <returns>Value from <see cref="System.Data.IDataReader"/>.</returns>
        object GetValue(System.Data.IDataReader dataReader);
    }
    /// <summary>
    /// Represents a object used to get the value from <see cref="System.Data.IDataReader"/> and change its type to T.
    /// </summary>
    /// <typeparam name="T">The type of Data</typeparam>
    public interface IDbColumnMapping<T> : IDbColumnMapping
    {
        /// <summary>
        /// Gets the value from <see cref="System.Data.IDataReader"/> and change its type to T.
        /// </summary>
        /// <param name="dataReader"><see cref="System.Data.IDataReader"/></param>
        /// <returns>Value from <see cref="System.Data.IDataReader"/>.</returns>
        new T GetValue(System.Data.IDataReader dataReader);
    }
}
