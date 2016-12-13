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
    BICreateUser BIGINT NOT NULL DEFAULT(0),
    BICreateTime DATETIME NOT NULL DEFAULT(DATETIME('NOW', 'LOCALTIME'))
)
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
";
            this.GetClient().ExecuteNonQuery(sqlText);
        }
    }
}
