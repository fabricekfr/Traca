using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Xml;

namespace DataAccess
{
    public class Class1
    {
        public void Load()
        {
            var xml = JsonConvert.DeserializeXmlNode(File.ReadAllText("C:/Users/fkwitond/Documents/Traca/TCServiceCenterBookingApplication/DataBase/centers.json"), "RootObject");
            var ds = new DataSet("Json Data");
            var xr = new XmlNodeReader(xml);
            ds.ReadXml(xr);

            var centerTypes = ds.Tables["CenterTypes"];

            foreach (DataRow centerTypesRow in centerTypes.Rows)
            {
                Console.WriteLine(centerTypesRow["Value"]);

                //var insertSQL = new SQLiteCommand("INSERT INTO CenterType (Id, Value) VALUES (?,?)", sql_con);
                //insertSQL.Parameters.Add(centerTypesRow["Id"]);
                //insertSQL.Parameters.Add(centerTypesRow["Value"]);
                //try
                //{
                //    insertSQL.ExecuteNonQuery();
                //}
                //catch (Exception ex)
                //{
                //    throw new Exception(ex.Message);
                //}

            }
        }

        public void InitializeDatabase()
        {
            using (var db = new SQLiteConnection("Data Source=TCDatabase.db;Version=3;"))
            {
                db.Open();

                var tableCommand = "CREATE TABLE IF NOT " + "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "Text_Entry NVARCHAR(2048) NULL)";

                var createTable = new SQLiteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

    }
}
