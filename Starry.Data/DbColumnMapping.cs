using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Starry.Data
{
    class DbColumnMapping<T>
    {
        public DbColumnMapping(DataTable schemaTable)
        {
            this.mappings = new Dictionary<string, PropertyInfo>();
            foreach (DataRow row in schemaTable.Rows)
            {
                var columnName = row[0].ToString(); //ColumnName
                var property = typeof(T).GetProperty(columnName) ??
                    typeof(T).GetProperty(columnName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.GetProperty);
                if (property != null && property.CanWrite)
                {
                    this.mappings[columnName] = property;
                }
            }
        }

        private IDictionary<string, PropertyInfo> mappings;

        public T GetValue(IDataReader dataReader)
        {
            var entity = Activator.CreateInstance<T>();
            foreach (var mapping in this.mappings)
            {
                var value = dataReader[mapping.Key];
                if (value == null || value is DBNull)
                {
                    mapping.Value.SetValue(entity, null, null);
                }
                else
                {
                    var underlyingType = Nullable.GetUnderlyingType(mapping.Value.PropertyType);
                    mapping.Value.SetValue(entity, Convert.ChangeType(value, underlyingType ?? mapping.Value.PropertyType), null);
                }
            }
            return entity;
        }
    }
}
