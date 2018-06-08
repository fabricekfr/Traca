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
            var sut = new Class1();
            sut.Load();
            //sut.InitializeDatabase();
        }
    }
}
