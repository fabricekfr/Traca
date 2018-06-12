// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

using ClientModel.DataAccessObjects;
using ClientModel.DomainObjects;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

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

        public override ICenterType MapRecord(SQLiteDataReader dataReader)
        {
            var centerType = _DomainObjectsFactory.CreateCenterType();
            centerType.Id = dataReader.GetInt32(dataReader.GetOrdinal(nameof(ICenterType.Id)));
            centerType.Value = (string) dataReader[nameof(ICenterType.Value)];
            return centerType;
        }
    }
}