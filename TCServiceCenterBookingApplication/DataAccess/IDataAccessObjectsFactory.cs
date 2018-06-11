using System.Data.SQLite;
using ClientModel.DataAccessObjects;

namespace DataAccess
{
    public interface IDataAccessObjectsFactory
    {
        SQLiteConnection GetConnection();
    }
}