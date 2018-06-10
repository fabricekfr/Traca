using System.Linq;
using System.Threading;
using ClientModel.DomainObjects;
using DataAccess;
using NSubstitute;
using NUnit.Framework;

namespace DataAccessTest
{
    [TestFixture]
    public class CenterTypeDAOTest
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
        }

        [Test, Apartment(ApartmentState.STA)]
        public void GetAll()
        {
            //Setup fixture
            var dataAccessObjectsFactory = TestHelpers.BuildDataAccessObjectsFactory();
            var domainObjectsFactory = Substitute.For<IDomainObjectsFactory>();
            var sut = new CenterTypeDAO(dataAccessObjectsFactory, domainObjectsFactory);

            //SUT
            var allRecords = sut.GetAll().ToList();

            //Verify outcomes
            var centerTypes = Helpers.LoadJsonFile().Tables["CenterTypes"];
            Assert.AreEqual(centerTypes.Rows.Count, allRecords.Count);
        }

        #endregion
    }
}