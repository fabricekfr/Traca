using System.Collections.Generic;
using ClientModel.DomainObjects;

namespace ClientModel.DataAccessObjects
{
    public interface ICenterDAO
    {
        IEnumerable<ICenter> GetAll();
        ICenter GetById(int id);
    }
}