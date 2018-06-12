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
                int id;
                if (int.TryParse(centerTypesRow["Id"].ToString(), out id))
                    commandText += InsertCenterType(id, centerTypesRow["Value"].ToString());
            }

            ExecuteCommand(commandText);
        }

        public void InitializeCentersTable()
        {
            var centers = Helpers.LoadJsonFile().Tables["Centers"];
            var commandText = string.Empty;

            foreach (DataRow centerTypesRow in centers.Rows)
            {
                int id, centerTypeId;
                int.TryParse(centerTypesRow["Id"].ToString(), out id);
                int.TryParse(centerTypesRow["CenterTypeId"].ToString(), out centerTypeId);

                commandText += InsertCenter(id, centerTypesRow["Name"].ToString(), centerTypesRow["StreetAddress"].ToString(), centerTypeId);
            }

            ExecuteCommand(commandText);
        }

        public void InitializeAppointmentsTable()
        {
            var commandText = "INSERT INTO Appointment (ClientFullName, [Date], Center) " +
                              "VALUES (\"John Doe\", \"2018-01-01\", 4);";
            commandText += "INSERT INTO Appointment (ClientFullName, [Date], Center) " +
                              "VALUES (\"Martine Duplessi\", \"2018-11-07\", 55);";

            ExecuteCommand(commandText);
        }

        #endregion

        #region Helpers

        private string InsertCenterType(int id, string value)
        {

            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (value == null) throw new ArgumentNullException(nameof(value));

            return $"INSERT INTO CenterType (Id, Value) VALUES ({id}, {FormatToSqLiteString(value)}); ";
        }

        private static string FormatToSqLiteString(string value)
        {
            return $"'{value.Replace("'", "''")}'";
        }

        private string InsertCenter(int id, string name, string streetAddress, int centerTypeId)
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

        private static void ExecuteCommand(string commandText)
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
