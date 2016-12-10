using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Starry.Data
{
    /// <summary>
    /// The DbClient is able to execute sql commands and manage connections and separate the read and write operations.
    /// </summary>
    public class DbClientWR : DbClient
    {
        /// <summary>
        /// Create a DbClient with database'name and read flag.
        /// </summary>
        /// <param name="dbName">The database'name</param>
        public DbClientWR(string dbName) : this(dbName, false) { }
        /// <summary>
        /// Create a DbClient with database'name and write/read flag.
        /// </summary>
        /// <param name="dbName">The database'name</param>
        /// <param name="isRead">The write/read flag, true is read.</param>
        public DbClientWR(string dbName, bool isRead) : base(dbName) { }
        /// <summary>
        /// Gets the write/read flag, true is read.
        /// </summary>
        public bool IsRead { private set; get; }
        /// <summary>
        /// Create a new connection from the database.
        /// </summary>
        /// <returns>A new connection from the database.</returns>
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
        /// <summary>
        /// Create a DbClient with database'name and read flag.
        /// </summary>
        /// <param name="dbName">The database'name</param>
        /// <returns>A new DbClient.</returns>
        public static DbClientWR CreateReadClient(string dbName)
        {
            return new DbClientWR(dbName, isRead: true);
        }
        /// <summary>
        /// Create a DbClient with database'name and write flag.
        /// </summary>
        /// <param name="dbName">The database'name</param>
        /// <returns>A new DbClient.</returns>
        public static DbClientWR CreateWriteClient(string dbName)
        {
            return new DbClientWR(dbName, isRead: false);
        }
    }
}
