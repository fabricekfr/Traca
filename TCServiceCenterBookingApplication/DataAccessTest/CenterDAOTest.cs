// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------


using ClientModel.DomainObjects;
using DataAccess;
using NSubstitute;
using NUnit.Framework;
using System.Linq;
using System.Threading;

namespace DataAccessTest
{
    [TestFixture]
    public class CenterDAOTest
    {
        #region Helpers
        #endregion

        #region Tests

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var tcDatabase = new TCDatabase();
            tcDatabase.InitializeDatabase();
            tcDatabase.InitializeCenterTypesTable();
            tcDatabase.InitializeCentersTable();
        }

        [Test, Apartment(ApartmentState.STA)]
        public void GetAll()
        {
            //Setup fixture
            var dataAccessObjectsFactory = TestHelpers.BuildDataAccessObjectsFactory();
            var domainObjectsFactory = Substitute.For<IDomainObjectsFactory>();
            var sut = new CenterDAO(dataAccessObjectsFactory, domainObjectsFactory);

            //SUT
            var allRecords = sut.GetAll().ToList();

            //Verify outcomes
            var centerTypes = Helpers.LoadJsonFile().Tables["Centers"];
            Assert.AreEqual(centerTypes.Rows.Count, allRecords.Count);
        }

        #endregion
    }
}