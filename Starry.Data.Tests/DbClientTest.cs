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
  FROM BLOGINFO
";
            var result = db.Query<Models.BlogInfo>(sqlText);
            Assert.True(result != null && result.Any());
        }

        [Fact]
        public void DbClientExecuteNonQueryTest()
        {
            var db = DbFixed.Instance.GetClient();
            var blogInfo = new Models.BlogInfo();
            var sqlText = @"
INSERT INTO
    BLOGINFO (
        BITitle,
        BIContent,
        BICreateUser)
    VALUES (
        @BITitle,
        @BIContent,
        @BICreateUser)
";
            blogInfo.BITitle = Guid.NewGuid().ToString();
            blogInfo.BIContent = Guid.NewGuid().ToString();
            blogInfo.BICreateUser = new Random((int)(DateTime.Now.Ticks % int.MaxValue)).Next(0, 10000);
            var count = db.ExecuteNonQuery(sqlText, blogInfo);
            Assert.True(count > 0);
        }

        [Fact]
        public void DbClientExecuteScalarTest()
        {
            var db = DbFixed.Instance.GetClient();
            var blogInfo = new Models.BlogInfo();
            {
                var sqlText = @"
INSERT INTO
    BLOGINFO (
        BITitle,
        BIContent,
        BICreateUser)
    VALUES (
        @BITitle,
        @BIContent,
        @BICreateUser);
SELECT LAST_INSERT_ROWID()
";
                blogInfo.BITitle = Guid.NewGuid().ToString();
                blogInfo.BIContent = Guid.NewGuid().ToString();
                blogInfo.BICreateUser = new Random((int)(DateTime.Now.Ticks % int.MaxValue)).Next(0, 10000);
                blogInfo.BIID = db.ExecuteScalar<int>(sqlText, blogInfo);
                Assert.True(blogInfo.BIID > 0);
            }
            {
                var sqlText = @"
SELECT *
  FROM BLOGINFO
 WHERE BIID = @BIID
";
                var result = db.Query<Models.BlogInfo>(sqlText, new { blogInfo.BIID });
                Assert.True(result != null && result.Any());
                var info = result.First();
                Assert.Equal(blogInfo.BIID, info.BIID);
                Assert.Equal(blogInfo.BITitle, info.BITitle);
                Assert.Equal(blogInfo.BIContent, info.BIContent);
                Assert.Equal(blogInfo.BICreateUser, info.BICreateUser);
            }
        }
    }
}
