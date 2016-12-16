using System;
using System.Collections.Generic;
using System.Text;

namespace Starry.Data
{
    /// <summary>
    /// DbBridge is a object which used to bridge Starry.Data's features.
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
        /// Gets and sets the factory used to generate <see cref="Starry.Data.IDbColumnMapping"/>
        /// </summary>
        public static IDbColumnMappingFactory MappingFactory
        {
            set { DbBridge.Instance.MappingFactory = value; }
            get { return DbBridge.Instance.MappingFactory; }
        }
    }
}
