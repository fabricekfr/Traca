using System;
using System.Data;
using System.Data.SQLite;

namespace DataAccess
{
    public class TCDatabase
    {
        public void InitializeDatabase()
        {
            SQLiteConnection.CreateFile($"{nameof(TCDatabase)}.db");
            using (var sqLiteConnection = new SQLiteConnection($"data source={nameof(TCDatabase)}.db"))
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

        public void Load()
        {
            var centerTypes = Helpers.LoadJsonFile().Tables["CenterTypes"];
            var commandText = string.Empty;
            foreach (DataRow centerTypesRow in centerTypes.Rows)
            {
                int id;
                if (int.TryParse(centerTypesRow["Id"].ToString(), out id))
                    commandText += InsertStatement(id, centerTypesRow["Value"].ToString());
            }

            using (var con = new SQLiteConnection("Data Source=TCDatabase.db;Version=3;"))
            {
                con.Open();

                var cmd = con.CreateCommand();
                cmd.CommandText = commandText;
                cmd.ExecuteNonQuery();
                con.Close();
            }

            foreach (DataRow centerTypesRow in centerTypes.Rows)
            {
                Console.WriteLine(centerTypesRow["Value"]);
            }
        }
        
        private string InsertStatement(int id, string value)
        {

            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (value == null) throw new ArgumentNullException(nameof(value));

            return $"INSERT INTO CenterType (Id, Value) VALUES ({id}, {FormatToSqLiteStreing(value)}); ";
        }

        private static string FormatToSqLiteStreing(string value)
        {
            return $"'{value.Replace("'", "''")}'";
        }

        private string InsertStatement2(int id, string value)
        {
            return $"INSERT INTO Center (Id, Value) VALUES ({id}, {value}); ";
        }
    }
}
