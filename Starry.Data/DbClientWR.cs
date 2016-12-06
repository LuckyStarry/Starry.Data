using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Starry.Data
{
    public class DbClientWR : DbClient
    {
        public DbClientWR(string dbName) : this(dbName, false) { }
        public DbClientWR(string dbName, bool isRead) : base(dbName) { }

        public bool IsRead { private set; get; }

        public override IDbConnection CreateDbConnection()
        {
            if (this.IsRead)
            {
                return DbHelper.CreateDbConnection(this.DBName + ".read");
            }
            else
            {
                return DbHelper.CreateDbConnection(this.DBName + ".write");
            }
        }

        public static DbClientWR CreateReadClient(string dbName)
        {
            return new DbClientWR(dbName, isRead: true);
        }

        public static DbClientWR CreateWriteClient(string dbName)
        {
            return new DbClientWR(dbName, isRead: false);
        }
    }
}
