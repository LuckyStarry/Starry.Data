using System;
namespace Starry.Data
{
    public interface IDbColumnMapping
    {
        object GetValue(System.Data.IDataReader dataReader);
    }

    interface IDbColumnMapping<T> : IDbColumnMapping
    {
        new T GetValue(System.Data.IDataReader dataReader);
    }
}
