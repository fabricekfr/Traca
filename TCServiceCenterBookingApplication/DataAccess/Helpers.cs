using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;
using Newtonsoft.Json;

namespace DataAccess
{
    public static class Helpers
    {
        public static string GetCreateDataBaseQuery()
        {
            const string resourceName = "DataAccess.Files.CreateDataBaseQuery.sql";
            return GetResourceContent(resourceName);
        }

        public static DataSet LoadJsonFile()
        {
            const string resourceName = "DataAccess.Files.centers.json";
            var xmlNode = JsonConvert.DeserializeXmlNode(GetResourceContent(resourceName), "RootObject");
            var dataSet = new DataSet("Json Data");
            var nodeReader = new XmlNodeReader(xmlNode);
            dataSet.ReadXml(nodeReader);
            return dataSet;
        }

        public static string GetExecutingAssemblyPath()
        {
            var executable = Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(executable);
            return path;
        }

        private static string GetResourceContent(string resourceName)
        {
            var value = string.Empty;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null) return value;
                var reader = new StreamReader(stream);
                value = reader.ReadToEnd();
            }

            return value;
        }
    }
}