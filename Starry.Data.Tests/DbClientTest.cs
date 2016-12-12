using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Starry.Data.Tests
{
    public class DbClientTest
    {
        [Fact]
        public void DbClientNameTest()
        {
            var dbName = Guid.NewGuid().ToString();
            var dbClient = new DbClient(dbName);
            Assert.Equal(dbName, dbClient.DBName);
        }

        [Fact]
        public void DbClientInit()
        {
            var db = new DbClient(Constants.DBNAME);
            using (var conn = db.CreateDbConnection())
            {

            }
        }
    }
}
