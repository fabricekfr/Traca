// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

using ClientModel.DomainObjects;
using System.Collections.Generic;

namespace ClientModel.DataAccessObjects
{
    public interface ICenterDAO
    {
        IEnumerable<ICenter> GetAll();
        ICenter GetById(int id);
    }
}