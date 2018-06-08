
using System.Data.SQLite;
using ClientModel.DataAccessObjects;

namespace DataAccess
{
    public class DataAccessObjectsFactory
    {
        private readonly string _DatabasePath;

        private DataAccessObjectsFactory()
        {
            _DatabasePath = $"{Helpers.GetExecutingAssemblyPath()}/{nameof(TCDatabase)}.db";
        }

        public static DataAccessObjectsFactory GetInstance()
        {
            return new DataAccessObjectsFactory();
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection($"Data Source={_DatabasePath};Version=3;");
        }

        public ICenterTypeDAO CreateCenterTypeDAO()
        {
            return new CenterTypeDAO(this);
        }
    }
}