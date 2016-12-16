/* MIT License
 * Copyright (c) 2016 Sun Bo
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
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
                var property = typeof(T).GetProperty(columnName);
                if (property == null)
                {
                    property = typeof(T).GetProperty(columnName, PROPERTY_BINGING_FLAGS);
                }
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
