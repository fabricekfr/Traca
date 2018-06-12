// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

using ClientModel.DomainObjects;

namespace Domain
{
    public class CenterType : ICenterType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
