using System;
using DataAccess;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DataAccessTest
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            var sut = new TCDatabase();
            sut.InitializeDatabase();
            sut.Load();
        }

        [Test]
        public void InitializeDatabase()
        {
            var sut = new TCDatabase();
            sut.InitializeDatabase();
        }
    }
}
