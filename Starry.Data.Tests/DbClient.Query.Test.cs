/* MIT License
 * Copyright (c) 2016 Sun Bo
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
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
