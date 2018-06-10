
using System;
using System.Data.SQLite;
using ClientModel.DataAccessObjects;

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

        public ICenterTypeDAO CreateCenterTypeDAO()
        {
            throw new NotImplementedException();
            return null; //new CenterTypeDAO(this);
        }
    }
}