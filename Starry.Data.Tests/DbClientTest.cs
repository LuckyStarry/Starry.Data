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
        public void DbClientQueryDbNullTest()
        {
            var db = DbFixed.Instance.GetClient();
            var sqlText = @"
SELECT *
  FROM DBNullTable
 WHERE ID = 1
";
            var result = db.Query<Models.DbNullEntity>(sqlText);
            Assert.True(result != null && result.Any());
            Assert.Equal(null, result.First().Value);
        }

        [Fact]
        public void DbClientQueryDbNullTest_Ex()
        {
            var db = DbFixed.Instance.GetClient();
            var sqlText = @"
SELECT *
  FROM DBNullTable
 WHERE ID = 1
";
            var result = db.Query<Models.DbNullEntityEx>(sqlText);
            Assert.True(result != null && result.Any());
            Assert.Equal(null, result.First().Value);
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

        [Fact]
        public void DbClientExecuteScalarReturnNullTest()
        {
            var db = DbFixed.Instance.GetClient();
            var sqlText = @"
SELECT BIID
  FROM BlogInfo
 WHERE BIID < 0
";
            var result = db.ExecuteScalar<int>(sqlText);
            Assert.Equal(0, result);
        }

        [Fact]
        public void DbClientExecuteTest()
        {
            var blogInfo = new Models.BlogInfo();
            blogInfo.BITitle = Guid.NewGuid().ToString();
            blogInfo.BIContent = Guid.NewGuid().ToString();
            blogInfo.BICreateUser = new Random((int)(DateTime.Now.Ticks % int.MaxValue)).Next(0, 10000);
            var db = DbFixed.Instance.GetClient();
            var info = db.Execute(connection =>
            {
                Assert.True(connection.State != System.Data.ConnectionState.Open);
                connection.Open();
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
                    var command = connection.CreateCommand();
                    command.CommandText = sqlText;

                    var biTitle = command.CreateParameter();
                    biTitle.ParameterName = @"BITitle";
                    biTitle.DbType = System.Data.DbType.String;
                    biTitle.Value = blogInfo.BITitle;
                    command.Parameters.Add(biTitle);

                    var biContent = command.CreateParameter();
                    biContent.ParameterName = @"BIContent";
                    biContent.DbType = System.Data.DbType.String;
                    biContent.Value = blogInfo.BIContent;
                    command.Parameters.Add(biContent);

                    var biCreateUser = command.CreateParameter();
                    biCreateUser.ParameterName = @"BICreateUser";
                    biCreateUser.DbType = System.Data.DbType.Int32;
                    biCreateUser.Value = blogInfo.BICreateUser;
                    command.Parameters.Add(biCreateUser);

                    var oBiid = command.ExecuteScalar();
                    Assert.NotNull(oBiid);

                    blogInfo.BIID = Convert.ToInt32(oBiid);
                    Assert.True(blogInfo.BIID > 0);
                }
                {
                    var sqlText = @"
SELECT *
  FROM BLOGINFO
 WHERE BIID = @BIID
";
                    var command = connection.CreateCommand();
                    command.CommandText = sqlText;

                    var biID = command.CreateParameter();
                    biID.ParameterName = @"BIID";
                    biID.DbType = System.Data.DbType.Int32;
                    biID.Value = blogInfo.BIID;
                    command.Parameters.Add(biID);

                    var blogInfos = new List<Models.BlogInfo>();
                    using (var dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entity = new Models.BlogInfo();
                            entity.BIID = Convert.ToInt32(dr["BIID"]);
                            entity.BITitle = Convert.ToString(dr["BITitle"]);
                            entity.BIContent = Convert.ToString(dr["BIContent"]);
                            entity.BICreateUser = Convert.ToInt32(dr["BICreateUser"]);
                            entity.BICreateTime = Convert.ToDateTime(dr["BICreateTime"]);
                            blogInfos.Add(entity);
                        }
                    }
                    Assert.True(blogInfos != null && blogInfos.Any());
                    return blogInfos.First();
                }
            });
            Assert.Equal(blogInfo.BIID, info.BIID);
            Assert.Equal(blogInfo.BITitle, info.BITitle);
            Assert.Equal(blogInfo.BIContent, info.BIContent);
            Assert.Equal(blogInfo.BICreateUser, info.BICreateUser);
        }
    }
}
