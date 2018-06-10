using System;
using System.Data;
using System.Data.SQLite;

namespace DataAccess
{
    public class TCDatabase
    {
        #region Public methods

        public void InitializeDatabase()
        {
            var databasePath = $"{Helpers.GetExecutingAssemblyPath()}/{nameof(TCDatabase)}.db";
            SQLiteConnection.CreateFile(databasePath);

            using (var sqLiteConnection = new SQLiteConnection($"data source={databasePath}"))
            {
                using (var sqLiteCommand = new SQLiteCommand(sqLiteConnection))
                {
                    sqLiteConnection.Open();
                    sqLiteCommand.CommandText = Helpers.GetCreateDataBaseQuery(); 
                    sqLiteCommand.ExecuteNonQuery();
                    sqLiteConnection.Close();
                }
            }

        }

        public void InitializeCenterTypesTable()
        {
            var centerTypes = Helpers.LoadJsonFile().Tables["CenterTypes"];
            var commandText = string.Empty;

            foreach (DataRow centerTypesRow in centerTypes.Rows)
            {
                uint id;
                if (uint.TryParse(centerTypesRow["Id"].ToString(), out id))
                    commandText += InsertCenterType(id, centerTypesRow["Value"].ToString());
            }

            CreateTable(commandText);
        }

        public void InitializeCentersTable()
        {
            var centers = Helpers.LoadJsonFile().Tables["Centers"];
            var commandText = string.Empty;

            foreach (DataRow centerTypesRow in centers.Rows)
            {
                uint id, centerTypeId;
                uint.TryParse(centerTypesRow["Id"].ToString(), out id);
                uint.TryParse(centerTypesRow["CenterTypeId"].ToString(), out centerTypeId);

                commandText += InsertCenter(id, centerTypesRow["Name"].ToString(), centerTypesRow["StreetAddress"].ToString(), centerTypeId);
            }

            CreateTable(commandText);
        }

        #endregion

        #region Helpers

        private string InsertCenterType(uint id, string value)
        {

            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (value == null) throw new ArgumentNullException(nameof(value));

            return $"INSERT INTO CenterType (Id, Value) VALUES ({id}, {FormatToSqLiteString(value)}); ";
        }

        private static string FormatToSqLiteString(string value)
        {
            return $"'{value.Replace("'", "''")}'";
        }

        private string InsertCenter(uint id, string name, string streetAddress, uint centerTypeId)
        {

            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (centerTypeId <= 0) throw new ArgumentOutOfRangeException(nameof(centerTypeId));
            if (name == null) throw new ArgumentNullException(nameof(name));
            streetAddress = string.IsNullOrWhiteSpace(streetAddress) ? string.Empty : streetAddress;

            return "INSERT INTO Center (Id, Name, StreetAddress, CenterTypeId) " +
                   $"VALUES ({id}, " +
                           $"{FormatToSqLiteString(name)}, " +
                           $"{FormatToSqLiteString(streetAddress)}, " +
                           $"{centerTypeId}); ";
        }

        private static void CreateTable(string commandText)
        {
            var databasePath = $"{Helpers.GetExecutingAssemblyPath()}/{nameof(TCDatabase)}.db";
            try
            {
                using (var connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion
    }
}
