using System;
using System.Collections.Generic;
using System.Data.SQLite;
using ClientModel.DataAccessObjects;
using ClientModel.DomainObjects;

namespace DataAccess
{
    public class CenterTypeDAO : BaseDAO<ICenterType>, ICenterTypeDAO
    {
        private readonly IDomainObjectsFactory _DomainObjectsFactory;

        public CenterTypeDAO(IDataAccessObjectsFactory daoFactory, IDomainObjectsFactory domainObjectsFactory) 
            : base(daoFactory)
        {
            if (domainObjectsFactory == null) throw new ArgumentNullException(nameof(domainObjectsFactory));
            _DomainObjectsFactory = domainObjectsFactory;
        }

        public IEnumerable<ICenterType> GetAll()
        {
            using (var command = new SQLiteCommand("SELECT * FROM CenterType"))
            {
                return GetRecords(command);
            }
        }

        public ICenterType GetById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            using (var command = new SQLiteCommand($"SELECT * FROM CenterType WHERE Id = {id}"))
            {
                return GetRecord(command);
            }
        }

        public void Add(ICenterType centerType)
        {
            throw new NotImplementedException();


            // $"INSERT INTO CenterType (Id, Value) VALUES ({id}, {FormatToSqLiteString(value)}); ";
        }

        public void AddRange(IEnumerable<ICenterType> centerTypes)
        {
            throw new NotImplementedException();
        }

        public override ICenterType MapRecord(SQLiteDataReader dataReader)
        {
            var centerType = _DomainObjectsFactory.CreateCenterType();
            centerType.Id = dataReader.GetInt32(dataReader.GetOrdinal(nameof(ICenterType.Id)));
            centerType.Value = (string) dataReader[nameof(ICenterType.Value)];
            return centerType;
        }
    }
}