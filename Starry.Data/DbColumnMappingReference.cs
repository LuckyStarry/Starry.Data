using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Starry.Data
{
    class DbColumnMappingReference<T> : DbColumnMapping<T> where T : class
    {
        private const BindingFlags PROPERTY_BINGING_FLAGS = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.GetProperty;

        public DbColumnMappingReference(DataTable schema)
        {
            this.mappings = new Dictionary<string, PropertyInfo>();
            foreach (DataRow row in schema.Rows)
            {
                //first column of schema is ColumnName
                var columnName = row[0].ToString();
                var property = typeof(T).GetProperty(columnName) ??
                    typeof(T).GetProperty(columnName, PROPERTY_BINGING_FLAGS);
                if (property != null && property.CanWrite)
                {
                    this.mappings[columnName] = property;
                }
            }
        }

        private IDictionary<string, PropertyInfo> mappings;
        public override T GetValue(System.Data.IDataReader dataReader)
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
