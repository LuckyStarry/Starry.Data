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
using System.Threading.Tasks;

namespace Starry.Data.Tests
{
    class DbFixed
    {
        private static readonly object syncLock = new object();
        private static DbFixed instance = null;
        public static DbFixed Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                        {
                            instance = new DbFixed();
                        }
                    }
                }
                return instance;
            }
        }

        private DbFixed()
        {
            this.dbClient = new DbClient(Constants.DBNAME);
            this.TablesInit();
            this.DataInit();
        }

        private DbClient dbClient;
        public IDbClient GetClient()
        {
            return this.dbClient;
        }

        private void TablesInit()
        {
            var sqlText = @"
DROP TABLE IF EXISTS BLOGINFO;
CREATE TABLE BLOGINFO (
    BIID INTEGER PRIMARY KEY AUTOINCREMENT,
    BITitle VARCHAR(50) NOT NULL DEFAULT(''),
    BIContent VARCHAR(8000) NOT NULL DEFAULT(''),
    BICreateUser INTEGER NOT NULL DEFAULT(0),
    BICreateTime DATETIME NOT NULL DEFAULT(DATETIME('NOW', 'LOCALTIME'))
);
DROP TABLE IF EXISTS DBNULLTABLE;
CREATE TABLE DBNULLTABLE (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Value INTEGER
);
";
            this.GetClient().ExecuteNonQuery(sqlText);
        }

        private void DataInit()
        {
            var sqlText = @"
INSERT INTO
    BLOGINFO (
        BITitle,
        BIContent,
        BICreateUser)
    VALUES (
        'Hello World',
        'This is a test content',
        1)
;
INSERT INTO DBNULLTABLE ( ID ) VALUES ( 1 )
";
            this.GetClient().ExecuteNonQuery(sqlText);
        }
    }
}
