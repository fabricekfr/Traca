using System.Collections.Generic;
using ClientModel.DomainObjects;

namespace ClientModel.DataAccessObjects
{
    public interface ICenterTypeDAO
    {
        IList<ICenterType> GetCenterTypes();
        ICenterType GetCenterType(uint id);
    }
}