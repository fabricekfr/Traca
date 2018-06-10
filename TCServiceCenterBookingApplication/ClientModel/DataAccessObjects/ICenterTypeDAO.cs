using System.Collections.Generic;
using ClientModel.DomainObjects;

namespace ClientModel.DataAccessObjects
{
    public interface ICenterTypeDAO
    {
        IEnumerable<ICenterType> GetAll();
        ICenterType GetById(uint id);
        void Add(ICenterType centerType);
        void AddRange(IEnumerable<ICenterType> centerTypes);
    }
}