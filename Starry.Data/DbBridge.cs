using System;
using System.Collections.Generic;
using System.Text;

namespace Starry.Data
{
    /// <summary>
    /// DbBridge is able to provide some features for database operation.
    /// </summary>
    public sealed partial class DbBridge : IDbBridge
    {
        private IDbColumnMappingFactory mappingFactory;
        IDbColumnMappingFactory IDbBridge.MappingFactory
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this.mappingFactory = value;
            }
            get { return this.mappingFactory; }
        }
        /// <summary>
        /// Gets the factory object used for make column mapping
        /// </summary>
        public static IDbColumnMappingFactory MappingFactory
        {
            set { DbBridge.Instance.MappingFactory = value; }
            get { return DbBridge.Instance.MappingFactory; }
        }
    }
}
