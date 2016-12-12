using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starry.Data.Tests
{
    class DbFixed
    {
        private static readonly object syncLock = new object();
        private static DbFixed instance = null;
        public static DbFixed Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                        {
                            instance = new DbFixed();
                        }
                    }
                }
                return instance;
            }
        }

        private void Init()
        {
            this.dbClient = new DbClient(Constants.DBNAME);
        }

        private DbClient dbClient;
        public IDbClient GetClient()
        {
            return this.dbClient;
        }
    }
}
