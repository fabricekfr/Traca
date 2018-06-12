// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

using System.Data.SQLite;

namespace DataAccess
{
    public interface IDataAccessObjectsFactory
    {
        SQLiteConnection GetConnection();
    }
}