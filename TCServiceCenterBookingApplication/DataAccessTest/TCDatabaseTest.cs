using System;
using System.Data.SQLite;
using System.Threading;
using DataAccess;
using NUnit.Framework;

namespace DataAccessTest
{
    [TestFixture]
    public class TCDatabaseTest
    {

        #region Helpers

        private int RowsCount(string tableName)
        {
            var count =0;
            var databasePath = $"{Helpers.GetExecutingAssemblyPath()}/{nameof(TCDatabase)}.db";
            using (var connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"SELECT Count(*) FROM {tableName}";
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    count = Convert.ToInt32(dataReader.GetValue(0).ToString());
                }
                dataReader.Close();
                connection.Close();
            }

            return count;
        }

        #endregion

        #region Tests

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var tcDatabase = new TCDatabase();
            tcDatabase.InitializeDatabase();
        }

        [Test, Apartment(ApartmentState.STA)]
        public void InitializeCenterTypesTable()
        {
            //Setup fixture
            var sut = new TCDatabase();

            //SUT
            sut.InitializeCenterTypesTable();

            //Verify outcomes
            var centerTypes = Helpers.LoadJsonFile().Tables["CenterTypes"];
            Assert.AreEqual(centerTypes.Rows.Count, RowsCount("CenterType"));
        }

        [Test, Apartment(ApartmentState.STA)]
        public void InitializeCentersTable()
        {
            //Setup fixture
            var sut = new TCDatabase();

            //SUT
            sut.InitializeCentersTable();

            //Verify outcomes
            var centerTypes = Helpers.LoadJsonFile().Tables["Centers"];
            Assert.AreEqual(centerTypes.Rows.Count, RowsCount("Center"));
        }

        #endregion
    }
}
