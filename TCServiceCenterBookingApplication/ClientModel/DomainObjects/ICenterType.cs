// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

namespace ClientModel.DomainObjects
{
    public interface ICenterType : IDomainObject
    {
        int Id { get; set; }
        string Value { get; set; }
    }
}