using System;
using System.Collections.Generic;
using System.Data.SQLite;
using ClientModel.DomainObjects;

namespace DataAccess
{
    public abstract class BaseDAO<T> where T : IDomainObject
    {
        private readonly IDataAccessObjectsFactory _DAOFactory;

        protected BaseDAO(IDataAccessObjectsFactory daoFactory)
        {
            if (daoFactory == null) throw new ArgumentNullException(nameof(daoFactory));
            _DAOFactory = daoFactory;
        }

        public abstract T MapRecord(SQLiteDataReader dataReader);

        protected IEnumerable<T> GetRecords(SQLiteCommand command)
        {
            var records = new List<T>();

            command.Connection = _DAOFactory.GetConnection();
            command.Connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                        records.Add(MapRecord(reader));
                }
                finally
                {
                    reader.Close();
                }
            }
            finally
            {
                command.Connection.Close();
            }
            return records;
        }

        protected T GetRecord(SQLiteCommand command)
        {
            var record = default(T);
            command.Connection = _DAOFactory.GetConnection();
            command.Connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        record = MapRecord(reader);
                        break;
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            finally
            {
                command.Connection.Close();
            }
            return record;
        }

        protected int AddRecord(SQLiteCommand command)
        {
            int numberOfRows;
            command.Connection = _DAOFactory.GetConnection();
            command.Connection.Open();
            try
            {
                numberOfRows = command.ExecuteNonQuery();
            }
            finally
            {
                command.Connection.Close();
            }
            return numberOfRows;
        }
    }
}