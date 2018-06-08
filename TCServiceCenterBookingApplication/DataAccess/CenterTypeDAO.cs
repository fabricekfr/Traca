using System;
using System.Collections.Generic;
using ClientModel.DataAccessObjects;
using ClientModel.DomainObjects;

namespace DataAccess
{
    public class CenterTypeDAO : ICenterTypeDAO
    {
        private readonly DataAccessObjectsFactory _DAOFactory;

        public CenterTypeDAO(DataAccessObjectsFactory daoFactory)
        {
            if (daoFactory == null) throw new ArgumentNullException(nameof(daoFactory));
            _DAOFactory = daoFactory;
        }

        public IList<ICenterType> GetCenterTypes()
        {
            throw new System.NotImplementedException();
        }

        public ICenterType GetCenterType(uint id)
        {
            throw new System.NotImplementedException();
        }
    }
}