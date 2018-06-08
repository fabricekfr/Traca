﻿using System;
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
            //var xml = JsonConvert.DeserializeXmlNode(File.ReadAllText("C:/Users/fkwitond/Documents/Traca/TCServiceCenterBookingApplication/DataBase/centers.json"), "RootObject");
            var xml = JsonConvert.DeserializeXmlNode(File.ReadAllText("centers.json"), "RootObject");
            var ds = new DataSet("Json Data");
            var xr = new XmlNodeReader(xml);
            ds.ReadXml(xr);

            var centerTypes = ds.Tables["CenterTypes"];
            string CommandText = string.Empty;
            foreach (DataRow centerTypesRow in centerTypes.Rows)
            {
                int id;
                if (int.TryParse(centerTypesRow["Id"].ToString(), out id))
                    CommandText += InsertStatement(id, centerTypesRow["Value"].ToString());
            }

            using (var con = new SQLiteConnection("Data Source=TCDatabase.db;Version=3;"))
            {
                con.Open();

                var cmd = con.CreateCommand();
                cmd.CommandText = CommandText;
                cmd.ExecuteNonQuery();
                con.Close();
            }

            foreach (DataRow centerTypesRow in centerTypes.Rows)
            {
                Console.WriteLine(centerTypesRow["Value"]);
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

        private string InsertStatement(int id, string value)
        {
            return $"INSERT INTO CenterType (Id, Value) VALUES ({id}, {value}); ";
        }

        private string InsertStatement2(int id, string value)
        {
            return $"INSERT INTO Center (Id, Value) VALUES ({id}, {value}); ";
        }


    }
}
