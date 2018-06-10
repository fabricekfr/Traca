using System.Data;
using System.IO;
using System.Reflection;
using System.Xml;
using Newtonsoft.Json;

namespace DataAccess
{
    public static class Helpers
    {
        public static string GetCreateDataBaseQuery()
        {
            return File.ReadAllText($"{GetExecutingAssemblyPath()}/Files/CreateDataBaseQuery.sql");
        }

        public static DataSet LoadJsonFile()
        {
            var xmlNode = JsonConvert.DeserializeXmlNode(File.ReadAllText($"{GetExecutingAssemblyPath()}/Files/centers.json"), "RootObject");
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
    }
}