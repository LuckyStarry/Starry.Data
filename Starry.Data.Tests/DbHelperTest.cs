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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Starry.Data.Tests
{
    public class DbHelperTest
    {
        [Fact]
        public void ConnectionStringNotExistsTest()
        {
            Assert.Throws<Exception>(delegate
            {
                var db = new DbClient(Guid.NewGuid().ToString());
                using (var connection = db.CreateDbConnection())
                {

                }
            });
        }

        [Fact]
        public void QueryIDbDataParameterTest()
        {
            var blogInfo = new Models.BlogInfo();
            blogInfo.BITitle = Guid.NewGuid().ToString();
            blogInfo.BIContent = Guid.NewGuid().ToString();
            blogInfo.BICreateUser = new Random((int)(DateTime.Now.Ticks % int.MaxValue)).Next(0, 10000);
            var info = DbFixed.Instance.GetClient().Execute(connection =>
            {
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
                    var parameters = new List<IDbDataParameter>();

                    var biTitle = command.CreateParameter();
                    biTitle.ParameterName = @"BITitle";
                    biTitle.DbType = System.Data.DbType.String;
                    biTitle.Value = blogInfo.BITitle;
                    parameters.Add(biTitle);

                    var biContent = command.CreateParameter();
                    biContent.ParameterName = @"BIContent";
                    biContent.DbType = System.Data.DbType.String;
                    biContent.Value = blogInfo.BIContent;
                    parameters.Add(biContent);

                    var biCreateUser = command.CreateParameter();
                    biCreateUser.ParameterName = @"BICreateUser";
                    biCreateUser.DbType = System.Data.DbType.Int32;
                    biCreateUser.Value = blogInfo.BICreateUser;
                    parameters.Add(biCreateUser);

                    var oBiid = DbFixed.Instance.GetClient().ExecuteScalar<int>(sqlText, parameters);
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
                    var biID = command.CreateParameter();
                    biID.ParameterName = @"BIID";
                    biID.DbType = System.Data.DbType.Int32;
                    biID.Value = blogInfo.BIID;

                    var blogInfos = DbFixed.Instance.GetClient().Query<Models.BlogInfo>(sqlText, biID);
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
