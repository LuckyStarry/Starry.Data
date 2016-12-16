using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Starry.Data.Tests
{
    public class DbClientQueryTest
    {
        [Fact]
        public void DbClientQueryToListTest()
        {
            var db = DbFixed.Instance.GetClient();
            var sqlText = @"
SELECT *
  FROM BLOGINFO
";
            var result = db.Query<Models.BlogInfo>(sqlText);
            Assert.True(result != null && result.Any() && result.ToList().Any());
        }

        [Fact]
        public void DbClientQueryIgnoreCaseTest()
        {
            var db = DbFixed.Instance.GetClient();
            var sqlText = @"
SELECT biid, bititle, bicontent, bicreateuser, bicreatetime
  FROM BLOGINFO
";
            var result = db.Query<Models.BlogInfo>(sqlText);
            Assert.True(result != null && result.Any() && result.ToList().Any());
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
        public void DbClientQueryDbNullTest_I()
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
        public void DbClientQueryDbNullTest_II()
        {
            var db = DbFixed.Instance.GetClient();
            var value = new Random().Next();
            var sqlText = string.Format(@"
SELECT ID, {0} AS Value
  FROM DBNullTable
 WHERE ID = 1
", value);
            var result = db.Query<Models.DbNullEntityEx>(sqlText);
            Assert.True(result != null && result.Any());
            Assert.Equal(value, result.First().Value);
        }

        [Fact]
        public void DbClientQueryToListIntTest()
        {
            var db = DbFixed.Instance.GetClient();
            var sqlText = @"
SELECT *
  FROM BLOGINFO
";
            var result = db.Query<int>(sqlText);
            Assert.True(result != null && result.Any());
            Assert.Equal(1, result.First());
        }
    }
}
