using System;
using System.Collections.Generic;
using System.Text;

namespace Starry.Data
{
    partial class DbBridge
    {
        private static readonly object syncLock = new object();
        private static DbBridge instance = null;
        /// <summary>
        /// Gets the DbBridge's singleton instance.
        /// </summary>
        public static IDbBridge Instance
        {

            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                        {
                            instance = new DbBridge();
                        }
                    }
                }
                return instance;
            }
        }

        private DbBridge()
        {
            this.mappingFactory = new DbColumnMappingFactory();
        }
    }
}
