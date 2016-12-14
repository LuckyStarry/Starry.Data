using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Starry.Data.Tests
{
    public class DbClientWRTest
    {
        private const string init_sql = @"
DROP TABLE IF EXISTS BLOGINFO;
CREATE TABLE BLOGINFO (
    BIID INTEGER PRIMARY KEY AUTOINCREMENT,
    BITitle VARCHAR(50) NOT NULL DEFAULT(''),
    BIContent VARCHAR(8000) NOT NULL DEFAULT(''),
    BICreateUser INTEGER NOT NULL DEFAULT(0),
    BICreateTime DATETIME NOT NULL DEFAULT(DATETIME('NOW', 'LOCALTIME'))
);
INSERT INTO
    BLOGINFO (
        BITitle,
        BIContent,
        BICreateUser)
    VALUES (
        'Hello World',
        'This is a test content',
        1)
";

        [Fact]
        public void CreateWRClientTest()
        {
            var db = new DbClientWR(Constants.DBNAME);
            Assert.False(db.IsRead);
        }

        [Fact]
        public void CreateReadClientTest()
        {
            var db = DbClientWR.CreateReadClient(Constants.DBNAME);
            db.ExecuteNonQuery(init_sql);
            var sqlText = @"
SELECT *
  FROM BLOGINFO
";
            var result = db.Query<Models.BlogInfo>(sqlText);
            Assert.True(result != null && result.Any());
        }

        [Fact]
        public void CreateWriteClientTest()
        {
            var db = DbClientWR.CreateWriteClient(Constants.DBNAME);
            db.ExecuteNonQuery(init_sql);
            var sqlText = @"
SELECT *
  FROM BLOGINFO
";
            var result = db.Query<Models.BlogInfo>(sqlText);
            Assert.True(result != null && result.Any());
        }
    }
}
