using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Starry.Data.Tests.NET35
{
    [TestClass]
    public class DbClientTest
    {
        [TestMethod]
        public void TestDbClientName()
        {
            var name = "Test";
            var client = new DbClient(name);
            Assert.AreEqual(name, client.DBName);
        }
    }
}
