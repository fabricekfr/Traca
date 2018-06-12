// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

using ClientModel.DomainObjects;
using System.Collections.Generic;


namespace ClientModel.DataAccessObjects
{
    public interface ICenterTypeDAO
    {
        IEnumerable<ICenterType> GetAll();
        ICenterType GetById(int id);
    }
}