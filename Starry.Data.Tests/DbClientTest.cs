using System;
using System.Collections.Generic;
using System.Linq;
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
        public void DbClientGetConnectionTest()
        {
            var db = DbFixed.Instance.GetClient();
            using (var conn = db.CreateDbConnection())
            {
                Assert.True(conn != null);
            }
        }

        [Fact]
        public void DbClientQueryTest()
        {
            var db = DbFixed.Instance.GetClient();
            var sqlText = @"
SELECT *
  FROM BLOGINFO";
            var result = db.Query<Models.BlogInfo>(sqlText);
            Assert.True(result != null && result.Any());
        }
    }
}
