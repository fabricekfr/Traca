// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

using System.Data.SQLite;

namespace DataAccess
{
    public class DataAccessObjectsFactory : IDataAccessObjectsFactory
    {
        private readonly string _DatabasePath;

        public DataAccessObjectsFactory()
        {
            _DatabasePath = $"{Helpers.GetExecutingAssemblyPath()}/{nameof(TCDatabase)}.db";
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection($"Data Source={_DatabasePath};Version=3;");
        }
    }
}