﻿// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

using ClientModel.DataAccessObjects;
using ClientModel.DomainObjects;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DataAccess
{
    public class CenterDAO : BaseDAO<ICenter>, ICenterDAO
    {
        private readonly IDomainObjectsFactory _DomainObjectsFactory;

        public CenterDAO(IDataAccessObjectsFactory daoFactory, IDomainObjectsFactory domainObjectsFactory) 
            : base(daoFactory)
        {
            if (domainObjectsFactory == null) throw new ArgumentNullException(nameof(domainObjectsFactory));
            _DomainObjectsFactory = domainObjectsFactory;
        }

        public IEnumerable<ICenter> GetAll()
        {
            const string sqlComandText = "SELECT Center.Id, Center.Name, Center.StreetAddress, Center.CenterTypeId, CenterType.Value AS CenterTypeValue " +
                                         "FROM Center INNER JOIN CenterType ON Center.CenterTypeId = CenterType.Id ";

            using (var command = new SQLiteCommand(sqlComandText))
            {
                return GetRecords(command);
            }
        }

        public ICenter GetById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            var sqlComandText = "SELECT Center.Id, Center.Name, Center.StreetAddress, Center.CenterTypeId, CenterType.Value AS CenterTypeValue " +
                                "FROM Center INNER JOIN CenterType ON Center.CenterTypeId = CenterType.Id " +
                               $"WHERE Center.Id = {id}";

            using (var command = new SQLiteCommand(sqlComandText))
            {
                return GetRecord(command);
            }
        }

        public override ICenter MapRecord(SQLiteDataReader dataReader)
        {
            var center = _DomainObjectsFactory.CreateCenter();
            center.Id = dataReader.GetInt32(dataReader.GetOrdinal(nameof(ICenter.Id)));
            center.Name = (string)dataReader[nameof(ICenter.Name)];
            center.StreetAddress = dataReader[nameof(ICenter.StreetAddress)].GetType() != typeof(DBNull) ? (string)dataReader[nameof(ICenter.StreetAddress)] : string.Empty;
            center.CenterTypeId = dataReader.GetInt32(dataReader.GetOrdinal(nameof(ICenter.CenterTypeId)));
            center.CenterTypeValue = (string)dataReader[nameof(ICenter.CenterTypeValue)];

            return center;
        }
    }
}